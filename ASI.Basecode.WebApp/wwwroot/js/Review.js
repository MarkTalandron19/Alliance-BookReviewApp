$(document).ready(function () {
    $('#AddReviewBtn').click(function () {
        jQuery.noConflict();

        $('#ReviewModal input[name="bookId"]').val(bookId);

        $('#ReviewModal').modal('show');
        $('#modalTitle').text('Post a Review');
        console.log("Button clicked");
        console.log("Book ID: ", bookId);
    });

    $('#ViewReviewsBtn').click(function () {
        jQuery.noConflict();

        $('#ViewReviewsModal input[name="bookId"]').val(bookId);

        $('#ViewReviewsModal').modal('show');
        $('#modalTitle').text('View All Reviews');
        console.log("Button clicked");
    });
});

$(document).ready(function () {
    $('.rating-stars .star').click(function () {
        $('.rating-stars .star').removeClass('fas').addClass('far');
        var selectedValue = parseInt($(this).attr('data-value'));
        for (var i = 1; i <= selectedValue; i++) {
            $('.rating-stars .star[data-value="' + i + '"]').removeClass('far').addClass('fas');
        }

        $('#rating').val(selectedValue);
    });
});