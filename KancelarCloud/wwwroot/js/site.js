//Вывод вложения справа
$('.CenterPosition').on('click', '.filevloj  img', function () {
    var row;
    var namefile = $(this).prop("title");
    $.ajax({
        url: '/Home/Imagetransfer',
        type: 'POST',
        dataType: "json",
        cache: true,
        data: { namefile: namefile },
        success: function (data) {
            switch (data.Type) {
                case "application/pdf":
                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<iframe class="RightImg" id=' + data.Id + ' style="visibility:visible" src=' + data.PathFileName + '>' + '</iframe>';
                    break;

                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<img class="RightImg" id=' + data.Id + ' style="visibility:visible" src="/images/Excel.jpg" >';
                    break;
                case "application/msaccess":
                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<img class="RightImg" id=' + data.Id + ' style="visibility:visible" src="/images/access.jpg" > ';
                    break;
                case "application/octet-stream":
                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<img class="RightImg" id=' + data.Id + ' style="visibility:visible" src="/images/rar.jpg" > ';
                    break;
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<img class="RightImg" id=' + data.Id + ' style="visibility:visible" src="/images/word.jpg" > ';
                    break;
                case "application/msword":
                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<img class="RightImg" id=' + data.Id + ' style="visibility:visible" src="/images/word.jpg" > ';
                    break;
                case "image/tiff":
                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<img class="RightImg" id=' + data.Id + ' style="visibility:visible; overflow:auto" src=' + data.PathFileName + '>' + '</iframe>';
                    break;

                default:

                    row = '<H5> Имя: ' + data.FileName + '</H5>' + '<p> Размер файла:' + data.SizeFile + '</p>' + '<p> Дата создания:' + data.EnterDate + '</p>' + '<img class="RightImg" id=' + data.Id + ' style="visibility:visible" src=' + data.PathFileName + ' > ';
            }
            var btn = '<div class="form-group"><button type="button" id="del" class="btndel"' + '>' + "Удалить" + '</button>' + '<input name="Id" id="deletedoc" type="hidden" value=' + data.Id + '>' + '</>' + '<a class="adel" href="/Home/DownloadFile/' + data.Id + '">' + "Скачать" + '</a>' + '<button type="button" id="prev" class=Preview' + '>' + "Просмотр" + '</><div>';
            $(".InRightPosition").html(row);

            $(".infoblock > form").html(btn);
            $('#EmSend').css('visibility', 'visible');
        }
    });
});

//Просмотр редактор

$('#deletef').on('click', '#prev', function () {
    
    $('#overlay').fadeIn(400, function () {
      
        var linkimage = $('.InRightPosition > img').prop("src");
        var linkiaframe = $('.InRightPosition > iframe').prop("src");
        if (linkimage === undefined)
        {
            $('#previewForm').css('display', 'block').html('<iframe id ="frame" src =' + linkiaframe + '>'+ '</iframe>').animate({ opacity: 1 }, 200);
        }
        else {
            $('#previewForm').css('display', 'block').css('background-image', 'url(' + linkimage + ')').css('background-position', 'center').css('background-repeat', 'no-repeat').animate({ opacity: 1 }, 200);
        }
     
    });
});

$('#close').click( function () {
    $('#previewForm').animate({ opacity: 0 }, 200, function () {
        $(this).css('display', 'none');
        $('#previewForm > iframe').remove();
        $('#previewForm').css('background-image', 'none');
        $('#overlay').fadeOut(400);
    });
});

//Удаление
$('#deletef').on('click', '#del', function () {
    var href;
    var id = $('#deletedoc').val();
    var page = $('.left-menu #selectpage').val();
    if ((page === "Облако") || (page = undefined)) {
        href = '/Home/Index';
    }
    else {
        href = '/Home/Trash';
    }
    $.ajax({
        url: '/Home/DeleteFile',
        type: 'Post',
        data: { id: id },
        success: function (data)
        {
            $('.RightPosition').remove();
            $('body').load(href + '#result', CompleteDelete);
            
        
        }
    });
});


//Успешное добавление

function CompleteAdd() {
    new Noty({
        type: 'success',
        layout: 'bottomCenter',
        text: 'Файл успешно загружен'
    }).show();
}
    //Успешное удаление
    function CompleteDelete() {
        new Noty({
            type: 'success',
            layout:'bottomCenter',
            text: 'Удаление выполнено успешно'
        }).show();
    }

    //Добавление файла
    function inputEvent() {
        var files = this.files;
        var datas = new FormData();
        datas.append("uploadFile", files[0]);
        $.ajax({
            url: '/Home/AddFile',
            type: 'POST',
            cache: false,
            processData: false, //Не обрабатывать файлы
            contentType: false,
            data: datas,
            success: function (data) {
                $('body').load('/Home/Index' + '#result', CompleteAdd)
            }
        });
    }
    //Добавление файла
    $('input[type=file]').on("change", inputEvent);
    //Поиск
    $('#searchf').click(function () {
        var result;
        var namefile = $('#Selected').val();
        $('.hover').remove();
        $.ajax({
            url: '/Home/Search',
            type: 'POST',
            dataType: "json",
            data: { namefile: namefile },
            success: function (data) {
                $.each(data, function (index, value) {
                    switch (value.Type) {
                        case "application/pdf":
                            result = '<div class="hover">' + '<div class="filevloj">' + '<img src="images/pdf3.jpg" id="Imagetransfer" title =' + value.FileName + '>' + '<a href=' + value.FileName + '>' + value.FileName + '</a>';
                            break;
                        case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                            result = '<div class="hover">' + '<div class="filevloj">' + '<img src="images/Excel.jpg" id="Imagetransfer" title =' + value.FileName + '>' + '<a href=' + value.FileName + '>' + value.FileName + '</a>';
                            break;
                        case "application/msaccess":
                            result = '<div class="hover">' + '<div class="filevloj">' + '<img src="images/access.jpg" id="Imagetransfer" title =' + value.FileName + '>' + '<a href=' + value.FileName + '>' + value.FileName + '</a>';
                            break;
                        case "application/octet-stream":
                            result = '<div class="hover">' + '<div class="filevloj">' + '<img src="images/rar.jpg" id="Imagetransfer" title =' + value.FileName + '>' + '<a href=' + value.FileName + '>' + value.FileName + '</a>';
                            break;
                        case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                            result = '<div class="hover">' + '<div class="filevloj">' + '<img src="images/word.jpg" id="Imagetransfer" title =' + value.FileName + '>' + '<a href=' + value.FileName + '>' + value.FileName + '</a>';
                            break;
                        case "application/msword":
                            result = '<div class="hover">' + '<div class="filevloj">' + '<img src="images/word.jpg" id="Imagetransfer" title =' + value.FileName + '>' + '<a href=' + value.FileName + '>' + value.FileName + '</a>';
                            break;

                        default:
                            result = '<div class="hover">' + '<div class="filevloj">' + '<img id = "Imagetransfer" title =' + value.FileName + '  src = ' + value.PathFileName + '>' + '<a href=' + value.FileName + '>' + value.FileName + '</a>';
                    }
                    $('.CenterPosition').prepend(result);
                });
            }
        });
    });
    /*Вызов Справочника*/
    $(document).ready(function () {
        $('#EmSend').on('click', '#org', function () {
            
            $('#result').hide('slow', loadContent);
        });

        function loadContent() {

            $('#result').load("/Home/Org", '', showNewContent);
        }

        function showNewContent() {

            $('#result').show('normal');
        }
        return false;
    });
    //отправка на один ящик
    $('#EmSend').on('click', '#Send', function () {
        var id = $('#deletedoc').val();
        var Email = $('#Email').val();
        $.ajax({
            url: '/Home/SendMessage',
            type: 'POST',
            data: { id: id, Email: Email },
            success: function (data) {
                Send();
            }
        });
    });

  

    /*корзина*/
    $('.LeftPosition').on('click', '#BasketFile', function () {
        $.ajax({
            url: '/Home/Trash',
            success: function (data) {
                $('body').load('/Home/Trash' + '#result', Trash)

            }
        });

    });
    function Trash() {
        new Noty({
            type: 'error',
            layout: 'bottomCenter',
            text: 'Вы находитесь в разделе Корзина'
        }).show();
    }
    /*все файлы*/
    $('.LeftPosition').on('click', '#AllFile', function () {
       
        $.ajax({
            url: '/Home/Index',
            success: function (data) {
                $('body').load('/Home/Index' + '#result', Allfiles)
            }
        });

    });

    function Allfiles() {
        new Noty({
            type: 'info',
            layout: 'bottomCenter',
            text: 'Вы находитесь в разделе Все Файлы'
        }).show();
    }

    /*Недавние*/
    $('.LeftPosition').on('click', '#ClockFile', function () {
        $.ajax({
            url: '/Home/Recently',
            success: function (data) {
                $('body').load('/Home/Recently' + '#result', Recently)
            }
        });
    });

    function Recently() {
        new Noty({
            type: 'info',
            layout: 'bottomCenter',
            text: 'Вы находитесь в разделе Недавние'
        }).show();
    }
    function Send() {
        new Noty({
            type: 'success',
            layout: 'bottomCenter',
            text: 'Выполнено успешно'
        }).show();
    }

    /*загрузить из хтги*/


    $('.LeftPosition').on('click', '#DownLoadHTGI', function () {
        $('#InHtgi').modal();

    });
    $('#InHtgi').on('click', '#BtnHtgi', function () {

        var num = $('#InputHTGI').val();
        $.ajax({
            url: '/Home/LoadHtgi/',
            data: { num: num },
            success: function (data) {
                $('body').load('/Home/Index' + '#result', CompleteAdd);
                $('#InHTGI').closest();
            }
        });
    });


    $('#Email').blur(function () {
        if ($(this).val() != '') {
            var pattern = /^([a-z0-9_\.-])+@[a-z0-9-]+\.([a-z]{2,4}\.)?[a-z]{2,4}$/i;
            if (pattern.test($(this).val())) {
                $(this).css({ 'border': '1px solid #569b44' });
      

            } else {
                $(this).css({ 'border': '1px solid #ff0000' });
            
            }
        }
        else {

            $(this).css({ 'border': '1px solid #ff0000' });
        

        }
    });
    






  

