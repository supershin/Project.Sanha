
var information = {
    init: () => {
        $("#search-unit").click(() => {
            information.searchUnit();
            return false;
        });

        $("#UsingCoupon").click(() => {
            information.usingCode();
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
                    console.log(resp.data);
                    $("#infoModal").modal('hide');
                    window.location.href = baseUrl + 'Information/Index?projectid=' + resp.data.ProjectId + '&unitid=' + (resp.data.UnitId || '') + '&contractno=' + (resp.data.ContractNo || '');
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
    usingCode: () => { 
        var data = {
            Id: $("#InfoId").val(),
            ProjectId: $("#ProjectId").val(),
            UnitId: $("#UnitId").val(),
            ProjectName: $("#ProjectName").val(),
            AddressNo: $("#Address").val(),
            TransferDate: $("#TransferDate").val(),
            CustomerName: $("#CustomerName").val(),
            CustomerMobile: $("#CustomerMobile").val(),
            CustomerEmail: $("#CustomerEmail").val(),
            ShopId: $("#ShopId").val(),
            Exp: $("#Exp").val(),
            Quota: $("#Quota").val()
        };
        console.log(data)
        $.ajax({
            url: baseUrl + "Information/UsingCode",
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                console.log(resp);
                if (resp.url) {
                    window.location.href = resp.url;
                }
            },
            error: function (xhr, status, error) {
                // do something
            },
            data:data
        });
        return false;
    }
}
