
var information = {
    init: () => {
        $("#search-unit").click(() => {
            information.searchUnit();
            return false;
        });
    },
    searchUnit: () => {
        var data = {
            ProjectId: $("#ProjectId").val(),
            Address: $("#Address").val(),
        };
        console.log(data)
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
    }
}
