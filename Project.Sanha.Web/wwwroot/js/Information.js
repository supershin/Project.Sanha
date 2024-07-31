
var information = {
    init: () => {
        $("#search-unit").click(() => {
            information.searchUnit();
            return false;
        });

        $(document).on('click', '.using-coupon-btn', function () {
            var shopId = $(this).data('shop-id');
            var shopData = $('.shop-data[data-shop-id="' + shopId + '"]');

            var data = {
                InfoId: shopData.data('info-id'),
                ProjectId: shopData.data('project-id'),
                UnitId: shopData.data('unit-id'),
                ProjectName: shopData.data('project-name'),
                Address: shopData.data('address'),
                TransferDate: shopData.data('transfer-date'),
                CustomerName: shopData.data('customer-name'),
                CustomerMobile: shopData.data('customer-mobile'),
                CustomerEmail: shopData.data('customer-email'),
                ShopId: shopId,
                Exp: shopData.data('exp'),
                Quota: shopData.data('quota'),
                UsedQuota: shopData.data('used-quota')
            };

            information.checkIn(data);
            return false;
        });

        $("#CheckIn").click(() => {
            var data = $("#checkInModal").data('checkInData');
            $("#checkInModal").modal('hide');
            information.createCheckIn(data);
            return false;
        });
    },
    searchUnit: () => {
        var data = {
            ProjectId: $("#ProjectId").val(),
            Address: $("#Address").val(),
        };
        $.ajax({
            url: baseUrl + 'Information/SearchUnit',
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                if (resp.success) {
                    $("#infoModal").modal('hide');
                    let param = btoa(resp.data.ProjectId + ':' + resp.data.UnitId + ':' + resp.data.ContractNo)

                    window.location.href = baseUrl + 'Information?param=' + param;
                } else {
                    $("#addressError").text(resp.message);
                }
            },
            error: function (xhr, status, error) {
                // do something
                alert(" Coding Error ")
            },
            data: data
        });
        return false;
    },
    usingCode: (data) => { 
        var data = {
            InfoId: data.InfoId,
            ProjectId: data.ProjectId,
            UnitId: data.UnitId,
            ProjectName: data.ProjectName,
            Address: data.Address,
            TransferDate: data.TransferDate,
            CustomerName: data.CustomerName,
            CustomerMobile: data.CustomerMobile,
            CustomerEmail: data.CustomerEmail,
            ShopId: data.ShopId,
            Exp: data.Exp,
            Quota: data.Quota
        };
        $.ajax({
            url: baseUrl + "Information/UsingCode",
            type: 'post',
            dataType: 'json',  // expecting HTML in response
            data: data,
            success: function (resp) {
                
            },
            error: function (xhr, status, error) {
                // handle error
            },
        });
        return false;
    },
    createCheckIn: (data) => {
        var formData = new FormData();

        formData.append('UnitShopId', data.InfoId);
        formData.append('ProjectId', data.ProjectId);
        formData.append('UnitId', data.UnitId);
        formData.append('ShopId', data.ShopId);
        formData.append('CustomerName', data.CustomerName);
        formData.append('CustomerMobile', data.CustomerMobile);
        formData.append('CustomerEmail', data.CustomerEmail);

        var files = document.getElementById('Images').files;
        for (var i = 0; i < files.length; i++) {
            formData.append('Image', files[i]);
        }

        $.ajax({
            url: baseUrl + "Information/CreateCheckIn",
            type: 'post',
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (resp) {
                if (resp.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'ทำการเช็คอินสำเร็จ',
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
            },
            error: function (xhr, status, error) {
                // do something
            },
            data: formData
        });
        return false;
    },
    checkIn: (data) => {
        $.ajax({
            url: baseUrl + "Information/CheckIn",
            type: 'POST',
            dataType: 'json',
            data: data, // Send data here
            success: function (resp) {
                if (resp.data === true) {
                    var queryString = $.param(data);
                    window.location.href = baseUrl + "Information/UsingCode?" + queryString;
                } else {
                    $("#checkInModal").data('checkInData', data).modal('show');
                }
            },
            error: function (xhr, status, error) {
                console.error("Error occurred: ", error);
            }
        });

        return false;
    },
}
