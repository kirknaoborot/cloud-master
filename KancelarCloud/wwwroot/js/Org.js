///*отправка на несколько адресов*/
$('.btn-manage').on('click', '#SendModal', function () {
    var Email = $('#check:checked').map(function () { return this.value; }).get();
    var id = $('#deletedoc').val();
    $.ajax({
        url: '/Home/SendMessage',
        type: 'POST',
        data: { id: id, Email: Email },
        success: function (data) {
            spr();
        }
    });
});
//модальное окно

$('.btn-manage').on('click', '#selectedSend', function () {
    if ($('input:checkbox').is(':checked')) {
        $('input:checkbox').prop('checked', false);
    } else {
        $('input:checkbox').prop('checked', true);
    }

});
//Удаление из справочника
$('#result').on('click', '#DeleteSpr', function () {
    var Id = $(this).val();
    $.ajax({
        url: '/Home/DeleteSpravochnik',
        type: 'POST',
        data: { Id: Id },
        success: function () {
            spr();
        }

    });
});

$('#result').on('click', '#UpdateSpr', function () {
    var id = $(this).val();
    $('[id=id]').css('visibility', 'visible');

});


$('body').on('keydown', '#searchspr', function () {
    var entertext = $('#searchspr').val();
    var td;
    $.ajax({
        url: '/Home/AutoSearchSpr',
       
        data: { nameorgspr: entertext },
        success: function (data) {
            $('#myTable').empty();
            $.each(JSON.parse(data), function (index,value) {
                td = '<tr>' + '<td class="text-center">' + '<input type="checkbox" name="Email[]" value=' + value.Email +' id="check">' + '</td>' + '<td class="text-center" id="DeleteOrg">' + '<button type="button" value=' + value.OrgId + ' id="DeleteSpr"><span><i class="fa fa-trash"></i></span>' + '</button>' + '</td>' + '<td class="text-center" id='+ value.OrgId +'>' + '<button type="button" value="' + value.NameOrg + '" id="UpdateSpr" title =' + value.Email + '><span><i class="fa fa-edit"></i></span>' + '</button>' + '</td>' + '<td>' + value.NameOrg + '</td></tr>';
                $('#myTable').prepend(td);
                //console.log(value);
            });
           
        }

    });
});

function spr() {
    $('#result').hide('slow', loadspr);
    function loadspr() {

        $('#result').load("/Home/Org", '', showNewspr);
    }

    function showNewspr() {

        $('#result').show('normal');
    }
}

$('#addOrg').on('click', '#btnaddorg', function () {

    var nameorg = $('#inputnameorg').val();
    var Email = $('#inputmail').val();
    $.ajax({
        url: '/Home/AddOrg',
        data: { Email: Email, nameorg: nameorg },
        success: function (data) {
            spr();
        }

    });

});



$('.CenterPosition').on('click', '#AddOrg', function () {
    $('#addOrg').modal();
});


$('.CenterPosition').on('click', '#UpdateSpr', function () {
    var name = $(this).val();
    var em = $(this).prop("title");
    var id = $(this).parent(this).attr("id");
    $('#updatenameorg').val(name);
    $('#updatemail').val(em);
    $('#inputid').val(id);
    $('#updateOrg').modal();

});
  
$('#updateOrg').on('click', '#btnupdorg', function () {
    
    var nameorg = $('#updatenameorg').val();
    var email = $('#updatemail').val();
    var idin = $('#inputid').val();
    $.ajax({
        url: '/Home/UdpateOrg',
        cashe: false,
        data: {idin: idin, nameorg: nameorg, email: email },
        success: function (data) {
            spr();
        }
    });
});