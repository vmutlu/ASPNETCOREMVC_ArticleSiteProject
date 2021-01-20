$(document).ready(function () {
    const dataTable = $('#usersTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd"
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-info',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/User/GetAllUsers/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#usersTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const userListDTO = jQuery.parseJSON(data);
                            if (userListDTO.ResultStatus === 0) {
                                $.each(userListDTO.Users.$values, function (index, user) {

                                    const newTableRow = dataTable.row.add([
                                        user.Id,
                                        user.UserName,
                                        user.Email,
                                        user.PhoneNumber,
                                        `<img src="/img/${user.Picture}" class="my-image-table"  alt="${user.UserName}"/>`,
                                        `
                                
                                    <button class="btn btn-info btn-sm btn-update" data-id="${user.Id}"><span class="fas fa-edit"></span> </button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${user.Id}"><span class="fas fa-minus-circle"></span> </button>
                                
                            `
                                    ]).node();
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${user.Id}`);

                                });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('usersTable').fadeIn(1400);
                            }
                            else {
                                toastr.error(`${userListDTO.Message}`, "İşlem Başarısız !");
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#usersTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, "Hata !");
                        }
                    });
                }
            }
        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi",
                    "0": "-"
                },
                "0": "-",
                "1": "%d satır seçildi",
                "2": "-",
                "_": "%d satır seçildi",
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            },
            "autoFill": {
                "cancel": "İptal",
                "fill": "Bütün hücreleri <i>%d<i> ile doldur<\/i><\/i>",
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "info": "-"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copy": "Koyala",
                "copyKeys": "Tablodaki sisteminize kopyalamak için CTRL veya u2318 + C tuşlarına basınız.",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "1": "-",
                    "_": "%d satır göster"
                },
                "pdf": "PDF",
                "print": "Yazdır"
            },
            "decimal": "-",
            "infoPostFix": "-",
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "clearAll": "Hepsini Kaldır",
                "condition": "Koşul",
                "conditions": {
                    "date": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "moment": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "number": {
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "gt": "Büyüktür",
                        "gte": "Büyük eşittir",
                        "lt": "Küçüktür",
                        "lte": "Küçük eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "string": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "endsWith": "İle biter",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "startsWith": "İle başlar"
                    }
                },
                "data": "Veri",
                "deleteTitle": "Filtreleme kuralını silin",
                "leftTitle": "Kriteri dışarı çıkart",
                "logicAnd": "ve",
                "logicOr": "veya",
                "rightTitle": "Kriteri içeri al",
                "title": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "value": "Değer"
            },
            "searchPanes": {
                "clearMessage": "Hepsini Temizle",
                "collapse": {
                    "0": "Arama Bölmesi",
                    "_": "Arama Bölmesi (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown}\/{total}",
                "emptyPanes": "Arama Bölmesi yok",
                "loadMessage": "Arama Bölmeleri yükleniyor ...",
                "title": "Etkin filtreler - %d"
            },
            "searchPlaceholder": "Ara",
            "thousands": "."
        }
    });
    //@* DataTables ends here *@

    $(function () {
        const url = '/Admin/User/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-user-add');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0)); // data tableda 0. indisteki verileri getirsin

            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {
                    console.log(data);
                    const userAddAjaxModel = jQuery.parseJSON(data);
                    console.log(userAddAjaxModel);
                    const newFormBody = $('.modal-body', userAddAjaxModel.UserAddPartial);

                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide'); // `` backtik işareti                       

                        const newTableRow =  dataTable.row.add([
                            userAddAjaxModel.UserDTO.User.Id,
                            userAddAjaxModel.UserDTO.User.UserName,
                            userAddAjaxModel.UserDTO.User.Email,
                            userAddAjaxModel.UserDTO.User.PhoneNumber,
                            `<img src="/img/${userAddAjaxModel.UserDTO.User.Picture}" class="my-image-table" alt="${userAddAjaxModel.UserDTO.User.UserName}"/>`,
                            `
                               
                                    <button class="btn btn-info btn-sm btn-update" data-id="${userAddAjaxModel.UserDTO.User.Id}"><span class="fas fa-edit"></span> </button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${userAddAjaxModel.UserDTO.User.Id}"><span class="fas fa-minus-circle"></span> </button>
                               
                            `
                        ]).node();

                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${userAddAjaxModel.UserDTO.User.Id}`);
                        dataTable.row(newTableRow).draw();

                        toastr.success(`${userAddAjaxModel.UserDTO.Message}`, 'Başarılı İşlem !');
                    }

                    else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });

                        toastr.warning(summaryText);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    });


    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const userName = tableRow.find('td:eq(1)').text(); // tablodaki 2. sutunu yani kullanıcı adını seçip değişkene attım.
        Swal.fire({
            title: 'Silmek istediginize emin misiniz?',
            text: `${userName} adlı kullanıcı silecektir !`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet !',
            cancelButtonText: 'Hayır !'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { userId: id },
                    url: '/Admin/User/Delete/',
                    success: function (data) {
                        const userDTO = jQuery.parseJSON(data);
                        if (userDTO.ResultStatus === 0) {
                            Swal.fire(
                                'Silindi !',
                                `${userDTO.User.User.Name} adlı kullanıcı başarıyla silinmiştir.`,
                                'success'
                            );
                            dataTable.row(tableRow).remove().draw();
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Hata ...',
                                text: `${userDTO.Message}`,
                            });
                        }
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(`${err.responseText}`, "Hata !");
                    }
                });
            }
        });

    });


    $(function () {
        const url = '/Admin/User/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { userId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Hata Oluştu");
            });
        });

        /* Ajax Update İşlemi */

        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();

            const form = $('#form-user-update');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0)); // data tableda 0. indisteki verileri getirsin

            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {

                    const userUpdateAjaxModel = jQuery.parseJSON(data);
                    if (userUpdateAjaxModel.userDTO != null) {
                        const id = userUpdateAjaxModel.UserDTO.User.Id;
                        const tableRow = $(`[name="${id}"]`);
                    }
                    const newFormBody = $('.modal-body', userUpdateAjaxModel.UserUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === "True";
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');

                        //
                        dataTable.row(tableRow).data([
                            userUpdateAjaxModel.UserDTO.User.Id,
                            userUpdateAjaxModel.UserDTO.User.UserName,
                            userUpdateAjaxModel.UserDTO.User.Email,
                            userUpdateAjaxModel.UserDTO.User.PhoneNumber,
                            `<img src="/img/${userUpdateAjaxModel.UserDTO.User.Picture}" class="my-image-table" alt="${userUpdateAjaxModel.UserDTO.User.UserName}"/>`,
                            `                               
                                    <button class="btn btn-info btn-sm btn-update" data-id="${userUpdateAjaxModel.UserDTO.User.Id}"><span class="fas fa-edit"></span> </button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${userUpdateAjaxModel.UserDTO.User.Id}"><span class="fas fa-minus-circle"></span> </button>                               
                            `
                        ]);
                        tableRow.attr("name", `${id}`);
                        dataTable.row(tableRow).invalidate();
                        //

                        toastr.success(`${userUpdateAjaxModel.UserDTO.Message}`, "İşlem Başarılı !");
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });

                        toastr.warning(summaryText);
                    }
                },
                error: function (err) {
                    console.log(err);
                    toastr.error(`${err.responseJSON}`,"Hata!");
                }
            });

        });

    });
});