$(document).ready(function () {
    $('#AddBtn').click(function () {
        jQuery.noConflict();

        $('#AddUserModal').modal('show');
        console.log("Button clicked");
    });

    $('.edit-button').click(function () {
        jQuery.noConflict();

        var genreId = $(this).data('genre-id');
        var genreName = $(this).data('genre-name');
        var description = $(this).data('genre-desc');



        $('#editGenreId').val(genreId);
        $('#editGenreName').val(genreName);
        $('#editDescription').val(description);
        $('#EditGenreModal').modal('show');
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
