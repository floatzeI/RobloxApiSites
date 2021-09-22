using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Roblox.Rendering.Client.Models;
using StackExchange.Redis;

namespace Roblox.Rendering.Client
{
    public class RenderingClient : IRenderingClient
    {
        private static ConnectionMultiplexer redisConnectionMultiplexer { get; set; }

        private static IDatabase redisDb => redisConnectionMultiplexer.GetDatabase(0);

        private const string renderChannelRequest = "RenderServiceRequest_V1";
        private const string renderChannelResponse = "RenderServiceResponse_V1";
        
        // pooling

        private static Mutex renderPoolMutex { get; set; } = new();

        private static List<RenderPoolEntry> renderPool { get; set; } = new();

        public RenderingClient(ConnectionMultiplexer redis)
        {
            redisConnectionMultiplexer = redis;
        }

        static RenderingClient()
        {
            RenderingClient.RedisSubscribeHandler();
        }

        private static async Task RedisSubscribeHandler()
        {
            while (redisConnectionMultiplexer == null)
            {
                await Task.Delay(100);
            }
            var con = redisConnectionMultiplexer.GetSubscriber();
            var channel = await con.SubscribeAsync(renderChannelResponse);
            channel.OnMessage(message =>
            {
                if (!message.Message.HasValue) return;
                var decoded = JsonSerializer.Deserialize<RenderResponse>(message.Message);
                if (decoded == null) return;
                // broadcast
                renderPoolMutex.WaitOne();
                foreach (var item in renderPool)
                {
                    if (item.id == decoded.id)
                    {
                        try
                        {
                            item.onFinish(decoded, message.Message);
                        }
                        catch (Exception)
                        {
                            // todo: log this somehow
                        }
                    }
                }
                renderPool = renderPool.Where(c => c.id != decoded.id).ToList();
                renderPoolMutex.ReleaseMutex();
            });
        }

        private async Task SendRequest(RenderRequest request)
        {
            await redisDb.PublishAsync(renderChannelRequest, JsonSerializer.Serialize(request));
        }
        
        public Task<RenderAvatarResponse> RenderAvatarThumbnail(RenderAvatarRequest request)
        { 
            var id = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<RenderAvatarResponse>();
            
            renderPoolMutex.WaitOne();
            renderPool.Add(new ()
            {
                id = id,
                parameters = new()
                {
                    request,
                },
                onFinish = (onFinish, rawBody) =>
                {
                    if (onFinish.status == 200)
                    {
                        tcs.SetResult(new ()
                        {
                            fileBase64 = onFinish.data,
                        });
                    }
                    else
                    {
                        tcs.SetException(new Exception("Result failed with Status = " + onFinish.status + "\nBody = " + rawBody + "\nID = " + onFinish.id));
                    }
                    return null;
                },
            });
            renderPoolMutex.ReleaseMutex();

            Task.Run(async () =>
            {
                await SendRequest(new RenderRequest()
                {
                    id = id,
                    args = new()
                    {
                        request,
                    },
                    command = "GenerateThumbnail",
                });
            });
            
            return tcs.Task;
        }
        
        public Task<RenderAvatarResponse> RenderAvatarHeadshot(RenderAvatarRequest request)
        { 
            var id = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<RenderAvatarResponse>();
            
            renderPoolMutex.WaitOne();
            renderPool.Add(new ()
            {
                id = id,
                parameters = new()
                {
                    request,
                },
                onFinish = (onFinish, rawBody) =>
                {
                    if (onFinish.status == 200)
                    {
                        tcs.SetResult(new ()
                        {
                            fileBase64 = onFinish.data,
                        });
                    }
                    else
                    {
                        tcs.SetException(new Exception("Result failed with Status = " + onFinish.status + "\nBody = " + rawBody + "\nID = " + onFinish.id));
                    }
                    return null;
                },
            });
            renderPoolMutex.ReleaseMutex();

            Task.Run(async () =>
            {
                await SendRequest(new RenderRequest()
                {
                    id = id,
                    args = new()
                    {
                        request,
                    },
                    command = "GenerateThumbnailHeadshot",
                });
            });
            
            return tcs.Task;
        }

        public Task<RenderAvatarResponse> RenderAsset(long assetId, int resolution)
        {
            var id = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<RenderAvatarResponse>();
            
            renderPoolMutex.WaitOne();
            renderPool.Add(new ()
            {
                id = id,
                parameters = new()
                {
                    assetId,
                },
                onFinish = (onFinish, rawBody) =>
                {
                    if (onFinish.status == 200)
                    {
                        tcs.SetResult(new ()
                        {
                            fileBase64 = onFinish.data,
                        });
                    }
                    else
                    {
                        tcs.SetException(new Exception("Result failed with Status = " + onFinish.status + "\nBody = " + rawBody + "\nID = " + onFinish.id));
                    }
                    return null;
                },
            });
            renderPoolMutex.ReleaseMutex();

            Task.Run(async () =>
            {
                await SendRequest(new RenderRequest()
                {
                    id = id,
                    args = new()
                    {
                        assetId,
                    },
                    command = "GenerateThumbnailHeadshot",
                });
            });
            
            return tcs.Task;
        }
    }
}