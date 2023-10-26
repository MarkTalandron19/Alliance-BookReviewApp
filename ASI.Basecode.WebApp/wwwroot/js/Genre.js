﻿$(document).ready(function () {
    $('#AddBtn').click(function () {
        jQuery.noConflict();
        
        $('#AddGenreModal').modal('show');
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

        $('#editGenreId').val(genreId);;
        $('#DeleteGenreModal').modal('show');
        console.log("Button clicked");
    });
    
});
