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
        var selectedRoles = $(this).data('user-role').split(',');

        $('#edituserId').val(userId);
        $('#editname').val(name);
        $('#editemail').val(email);
        $('#originalemail').val(email);
        $('#editpassword').val(password);
        $('#editconfirmpassword').val(password);
        selectedRoles.forEach(function (role) {
            $('#EditUserModal').find(':checkbox[value="' + role + '"]').prop('checked', true);
        });

        $('#EditUserModal').modal('show');
        console.log("Button clicked");
    });

    $('.delete-button').click(function () {
        jQuery.noConflict();

        var userId = $(this).data('user-id');

        $('#deleteuserid').val(userId);
        $('#DeleteUserModal').modal('show');
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
