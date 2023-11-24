$(document).ready(function () {
    $('#AddBtn').click(function () {
        jQuery.noConflict();

        $('#AddUserModal').modal('show');
        console.log("Button clicked");
    });

    $('.edit-button').click(function () {
        jQuery.noConflict();

        var userId = $(this).data('user-id');
        var name = $(this).data('user-name');
        var email = $(this).data('user-email');
        var password = $(this).data('user-password');



        $('#edituserId').val(userId);
        $('#editname').val(name);
        $('#originalemail').val(email);
        $('#editemail').val(email);
        $('#originalemail').val(email);
        $('#editpassword').val(password);
        $('#editconfirmpassword').val(password);
        $('#EditUserModal').modal('show');
        console.log("Button clicked");
    });

    $('.delete-button').click(function () {
        jQuery.noConflict();

        var genreId = $(this).data('genre-id');

        $('#deleteGenreId').val(genreId);
        $('#DeleteGenreModal').modal('show');
        console.log("Button clicked");
    });

    $(".genre-link").click(function () {
        // Get the selected genre ID
        var genreId = $(this).data("genre-id");

        // Hide all book lists initially
        $(".book-list").hide();

        // Show the book list for the selected genre
        $("#Booklist-genre-" + genreId).show();
    });

});
