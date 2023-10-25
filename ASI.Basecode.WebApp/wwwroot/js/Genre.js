/*
$(document).ready(function () {
    GetGenres();
})
function GetGenres() {
    $.ajax({
        url: '/genres/get',
        type: 'get',
        datatype: 'json',
        contentType: 'application/json',
        success: function (response) {
            console.log(response); 
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="3">' + '<center>No Genres created.</center>' + '</td>';
                object += '<tr>';
                $('#tableBody').html(object);
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.genreName + '</td>';
                    object += '<td>' + item.description + '</td>';
                    object += '<td> <a href="#" class="edit-button" onclick="Edit(\'' + item.genreId + '\')">Edit</a> <a href="#" class="delete-button" onclick="Delete(\'' + item.genreId + '\')">Delete</a> </td>';
                    object += '<tr>';
                });
                $('#tableBody').html(object);

            }
        },
        error: function () {
            alert('Unable to retrieve the data.');
        }
    })
}*/

$(document).ready(function () {
    $('#AddBtn').click(function () {
        jQuery.noConflict();
        
        $('#GenreModal').modal('show');
        $('#modalTitle').text('Add a Book Genre');
        console.log("Button clicked");
    });
    /*
    $('#Update').click(function () {
        jQuery.noConflict();

        $('#GenreModal').modal('show');
        $('#modalTitle').text('Update Book Genre Details');
        $('#Save').css('display', 'none');
        $('#Update').css('display', 'block');
        $('#genreId').val('');
        console.log("Button clicked");
    });
    */
});



function hideModal() {
    $('#GenreModal').modal('hide');
}

function GetGenreEdit() {
    $.ajax({
        url: '/genres/update=' + genreId,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            if (response == null || response == undefined) {
                alert('Unable to retrieve the data.');
            }
            else if (response.length == 0) {
                alert('data not available with the id' + genreId);
            }
            else {
                $('#GenreModal').modal('show');
                $('#modalTitle').text('Update Book Genre Details');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#genreId').val(response.genreId);
                $('#genreName').val(response.genreName);
                $('#description').val(response.description);
                console.log("Button clicked");
            }
        },
        error: function () {
            alert('Unable to retrieve the data.');
        }
    })
}
