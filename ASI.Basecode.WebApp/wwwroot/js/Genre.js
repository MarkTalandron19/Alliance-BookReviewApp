﻿$(document).ready(function () {
    GetGenres();
})
function GetGenres() {
    $.ajax({
        url: '/genres/get',
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="3">' + 'No Genres created.' + '</td>';
                object += '<tr>';
                $('#tableBody').html(object);
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.genreId + '</td>';
                    object += '<td>' + item.genreName + '</td>';
                    object += '<td>' + item.description + '</td>';
                    object += '<td> <a href="#" class="edit-button" onclick="Edit(' + item.genreId + ')">Edit</a> <a href="#" class="delete-button" onclick="Delete(' + item.genreId + ')">Delete</a> </td>';
                    object += '<tr>';
                    $('#tableBody').html(object);
                });
                $('#tableBody').html(object);

            }
        },
        error: function () {
            alert('Unable to retrieve the data.');
        }
    })
}
$('#AddBtn').click(function () {
    jQuery.noConflict();
    $('#GenreModal').modal('show');
    $('#modalTitle').text('Add a Book Genre');
}); 

function AddGenre() {
    var formData = new Object();
    formData.id = $('#genreId').val();
    formData.genreName = $('#genreName').val();
    formData.description = $('#description').val();

    $.ajax({
        url: '/genres/add',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save the data.');
            }
            else {
                alert('added');
            }
        },
        error: function () {
            alert('Unable to save the data.');
        }
    })
}

