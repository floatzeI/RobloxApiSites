$(function() {
    var docWarning = $("#doc-warning").first();
    var hidden = localStorage.getItem("doc_warning_hidden") === "true";
    if (!hidden) {
        docWarning.removeAttr("style");
    }
    $(document).on("click", ".warning-close", function(e) {
        e.preventDefault();
        localStorage.setItem("doc_warning_hidden", "true");
    });
});