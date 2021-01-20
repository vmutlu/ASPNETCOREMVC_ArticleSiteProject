$(document).ready(function () {
    $('#categoriesTable').DataTable({
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
                        url: '/Admin/Category/GetAllCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const categoryListDTO = jQuery.parseJSON(data);
                            if (categoryListDTO.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(categoryListDTO.Categories.$values, function (index, category) {
                                    tableBody +=
                                        `
                                                        <tr name=${category.Id}>
                                                            <td>${category.Id}</td>
                                                            <td>${category.Name}</td>
                                                            <td>${category.Description}</td>
                                                            <td>${category.IsActive ? "EVET" : "HAYIR"}</td>
                                                            <td>${category.IsDeleted ? "EVET" : "HAYIR"}</td>
                                                            <td>${category.Note}</td>
                                                            <td>${convertToShortDate(category.CreatedDate)}</td>
                                                            <td>${category.CreatedByName}</td>
                                                            <td>${convertToShortDate(category.ModifiedDate)}</td>
                                                            <td>${category.ModifiedByName}</td>
                                                            <td>
                                                            <button class="btn btn-info btn-sm btn-update" data-id="${category.Id}"><span class="fas fa-edit"></span> </button>
                                                            <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span> </button>
                                                            </td>
                                                       </tr>
                                                `;
                                });
                                $('#categoriesTable > tbody').replaceWith(tableBody);
                                $('.spinner-border').hide();
                                $('#categoriesTable').fadeIn(1400);
                            }
                            else {
                                toastr.error(`${categoryListDTO.Message}`, "İşlem Başarısız !");
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(1000);
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
        const url = '/Admin/Category/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-category-add');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const categoryAddAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAddPartial);

                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide'); // `` backtik işareti
                    const newTableRow =
                        `
                            <tr name="${categoryAddAjaxModel.CategoryDTO.Category.Id}">
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.Id}</td>
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.Name}</td>
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.Description}</td>
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.IsActive ? "EVET" : "HAYIR"}</td>
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.IsDeleted ? "EVET" : "HAYIR"}</td>
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.Note}</td>
                                <td>${convertToShortDate(categoryAddAjaxModel.CategoryDTO.Category.CreatedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.CreatedByName}</td>
                                <td>${convertToShortDate(categoryAddAjaxModel.CategoryDTO.Category.ModifiedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDTO.Category.ModifiedByName}</td>
                                <td>
                                <button class="btn btn-info btn-sm btn-update" data-id="${categoryAddAjaxModel.CategoryDTO.Category.Id}"><span class="fas fa-edit"></span> </button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDTO.Category.Id}"><span class="fas fa-minus-circle"></span> </button>
                                </td>
                            </tr>
                             `;

                    const newTableRowObject = $(newTableRow);
                    newTableRowObject.hide();
                    $('#categoriesTable').append(newTableRowObject);
                    newTableRowObject.fadeIn(3500); // tabloya veri eklendiği zaman anında görüntülenmeyecek slyt yüklenir gibi gelecek

                    toastr.success(`${categoryAddAjaxModel.CategoryDTO.Message}`, 'Başarılı İşlem !');
                }

                else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText = `*${text}\n`;
                    });

                    toastr.warning(summaryText);
                }
            });
        });
    });


    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const categoryName = tableRow.find('td:eq(1)').text(); // tablodaki 2. sutunu yani kategory adını seçip değişkene attım.
        Swal.fire({
            title: 'Silmek istediginize emin misiniz?',
            text: `${categoryName} adlı kategori silecektir !`,
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
                    data: { categoryId: id },
                    url: '/Admin/Category/Delete/',
                    success: function (data) {
                        const categoryDTO = jQuery.parseJSON(data);
                        if (categoryDTO.ResultStatus === 0) {
                            Swal.fire(
                                'Silindi !',
                                `${categoryDTO.Category.Name} adlı kategori başarıyla silinmiştir.`,
                                'success'
                            );
                            tableRow.fadeOut(3500);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Hata ...',
                                text: `${categoryDTO.Message}`,
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
        const url = '/Admin/Category/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { categoryId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Hata Oluştu");
            });
        });

        /* Ajax Update İşlemi */

        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();

            const form = $('#form-category-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {

                const categoryUpdateAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === "True";
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');

                    const newTableRow =
                        `
                             <tr name="${categoryUpdateAjaxModel.CategoryDTO.Category.Id}">
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.Id}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.Name}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.Description}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.IsActive ? "EVET" : "HAYIR"}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.IsDeleted ? "EVET" : "HAYIR"}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.Note}</td>
                                <td>${convertToShortDate(categoryUpdateAjaxModel.CategoryDTO.Category.CreatedDate)}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.CreatedByName}</td>
                                <td>${convertToShortDate(categoryUpdateAjaxModel.CategoryDTO.Category.ModifiedDate)}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDTO.Category.ModifiedByName}</td>
                                <td>
                                <button class="btn btn-info btn-sm btn-update" data-id="${categoryUpdateAjaxModel.CategoryDTO.Category.Id}"><span class="fas fa-edit"></span> </button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryUpdateAjaxModel.CategoryDTO.Category.Id}"><span class="fas fa-minus-circle"></span> </button>
                                </td>
                            </tr>
                        `;

                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${categoryUpdateAjaxModel.CategoryDTO.Category.Id}"]`);
                    newTableRowObject.hide();
                    categoryTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(3500);

                    toastr.success(`${categoryUpdateAjaxModel.CategoryDTO.Message}`, "İşlem Başarılı !");
                } else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText = `*${text}\n`;
                    });

                    toastr.warning(summaryText);
                }

            }).fail(function (response) {
                console.log(response);
            });

        });

    });
});