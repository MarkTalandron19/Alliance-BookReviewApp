$(document).ready(function () {
    $('#AddBookBtn').click(function () {
        jQuery.noConflict();

        $('#BookModal').modal('show');
        $('#modalTitle').text('Add a Book');
        console.log("Button clicked");
    });

    $('.edit-button').click(function () {
        jQuery.noConflict();

        var bookId = $(this).data('book-id');
        var title = $(this).data('book-title');
        var synopsis = $(this).data('book-synopsis');
        var publisher = $(this).data('book-publisher');
        var pubYear = $(this).data('book-pubYear');
        var isbn = $(this).data('book-isbn');
        var language = $(this).data('book-language');
        var genre = $(this).data('book-genre');

        $('#editBookId').val(bookId);
        $('#editTitle').val(title);
        $('#editSynopsis').val(synopsis);
        $('#editPublisher').val(publisher);
        $('#editPubYear').val(pubYear);
        $('#editIsbn').val(isbn);
        $('#editLanguage').val(language);
        $('#editGenre').val(genre);
        $('#EditBookModal').modal('show');
    });


    $('.delete-button').click(function () {
        jQuery.noConflict();

        var bookId = $(this).data('book-id');

        $('#deleteBookId').val(bookId);
        $('#DeleteBookModal').modal('show');
        console.log("Button clicked");
    });
});