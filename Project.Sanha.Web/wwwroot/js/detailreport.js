const detail = {
    init: (transId, juristicId) => {
        $('#approve').click(() => {
            $('#approve-modal').modal('show');
            return false;
        });

        $('#not-approved').click(() => {
            $('#not-approved-modal').modal('show');
            return false;
        });

        $('#confirm-approve').click(() => {
            var status = 2;
            console.log("state 1")
            detail.approveReport(transId, juristicId, status);
            
            return false;
        })

        //document.getElementById("confirm-approve").addEventListener("click", function () {
        //    Swal.fire({
        //        icon: 'success',
        //        title: 'ทำรายการสำเร็จ',
        //        showConfirmButton: false,
        //        timer: 1500
        //    });
        //    var id = 1
        //    detail.createReport(id)

             
        //});

        $('#confirm-not-approved').click(() => {
            var status = 3;
            var note = $('#not-arpproved-note').val();
            debugger;
            detail.approveReport(transId, juristicId, status, note);
            return false;
        })
    },

    AjaxGrid: function (id, status, note) {
        var data = {
            ProjectId: $("#ProjectId").val(),
            Address: $("#Address").val(),
        };
        $.ajax({
            url: baseUrl + '',
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                
            },
            error: function (xhr, status, error) {
                // do something
                alert(" Coding Error ")
            },
            data: data
        });
        return false;
    },
    createReport: function (id, authId) {
        var data = {
            transId: id,
            juristicId: authId
        };
        $.ajax({
            url: baseUrl + 'Report/RptShopService',
            type: 'post',
            dataType: 'json',
            data: data,
            success: function (resp) {
                console.log("state 3")
                if (resp.success) {
                    window.open(baseUrl + resp.data.Path, "_blank");

                    window.location.href = baseUrl + 'Approve/Index/' + resp.data.JuristicID;
                }
            },
            error: function (xhr, status, error) {
                // do something
                alert(" Coding Error ")
            },
        });
        return false;
    },
    approveReport: function (transId, juristicId, status, note) {
        var data = {
            TransID: transId,
            AuthenID: juristicId,
            Note: note,
            Status: status
        };
        $.ajax({
            url: baseUrl + 'Approve/ApproveTrans',
            type: 'post',
            dataType: 'json',
            data: data,
            success: function (resp) {
                if (resp.success) {
                    console.log("state 2")
                    if (resp.data.Status == 2) {

                        Swal.fire({
                            icon: 'success',
                            title: 'ทำรายการสำเร็จ',
                            showConfirmButton: false,
                            timer: 1500
                        });

                        detail.createReport(resp.data.TransID, resp.data.JuristicID);
                    }
                    else {
                        window.location.href = baseUrl + 'Approve/Index/' + resp.data.JuristicID;
                    }
                }
            },
            error: function (xhr, status, error) {
                // do something
                alert(" Coding Error ")
            },
        });
        return false;
    }
}