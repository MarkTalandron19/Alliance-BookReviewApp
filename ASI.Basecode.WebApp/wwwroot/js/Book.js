$(document).ready(function () {
    $('#AddBookBtn').click(function () {
        jQuery.noConflict();

        $('#BookModal').modal('show');
        $('#modalTitle').text('Add a Book');
        console.log("Button clicked");
    });
});