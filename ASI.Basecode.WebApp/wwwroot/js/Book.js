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
        var author = $(this).data('book-author');
        var synopsis = $(this).data('book-synopsis');
        var publisher = $(this).data('book-publisher');
        var isbn = $(this).data('book-isbn');
        var language = $(this).data('book-language');

        $('#editBookId').val(bookId);
        $('#editTitle').val(title);
        $('#editAuthor').val(author);
        $('#editSynopsis').val(synopsis);
        $('#editPublisher').val(publisher);
        $('#editIsbn').val(isbn);
        $('#editLanguage').val(language);
        $('#EditBookModal').modal('show');
        console.log("Button clicked");
    });

    $('.delete-button').click(function () {
        jQuery.noConflict();

        var bookId = $(this).data('book-id');

        $('#deleteBookId').val(bookId);
        $('#DeleteBookModal').modal('show');
        console.log("Button clicked");
    });
});