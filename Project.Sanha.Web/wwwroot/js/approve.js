
const approve = {
    init: (jId)=> {
        //$("button[data-action='list-ap-detail']").unbind('click').click((e) => {
        //    //alert(baseUrl);
        //    alert("func");
        //    let id = $(e.currentTarget).attr('data-id');
        //    alert(id);
        //    window.location.href = baseUrl + 'Approve/Detail';
        //});
        if ($.fn.DataTable.isDataTable('#tbl-table')) {
            $('#tbl-table').DataTable().clear().destroy();
        }

        $('#btnLoad').unbind('click').click((e) => {
            alert("a");
        });


        $('#export-excel').click(() => {
            Swal.fire({
                title: "Are you sure?",
                text: "Confirm to Export Excel",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm"
            }).then((result) => {
                if (result.isConfirmed) {
                    var selectedProject = $('#select-project').val();
                    var selectedStatus = $('#select-status').val();
                    var selectFrom = $('#date-from').val();
                    var selectTo = $('#date-to').val();

                    debugger;
                    var data = {
                        juristicID: jId,
                        projectID: selectedProject,
                        status: selectedStatus,
                        validFrom: selectFrom,
                        validThrough: selectTo
                    }

                    
                    $.ajax({
                        url: baseUrl + `Approve/ExcelUnitMaster`,
                        type: "GET",
                        dataType: 'json',
                        data: data,
                        success: function (resp) {
                            if (resp.success) {
                                // Redirect to download the file
                                //window.open(baseUrl + resp.fileUrl, "_blank");
                                window.location = baseUrl + resp.fileUrl;
                                //Swal.fire("Exported!", "Your Excel file is being downloaded.", "success");
                            } else {
                                Swal.fire("Error!", "Failed to generate Excel file.", "error");
                            }
                        },
                        error: () => {
                            Swal.fire("Error!", "There was an issue exporting the Excel file.", "error");
                        }
                    });
                }
            });
        });

        $("#btn-search").click(() => {
            if ($.fn.DataTable.isDataTable('#tbl-table')) {
                $('#tbl-table').DataTable().clear().destroy();
            }
            var selectedValue = $('#select-project').val();
            var selectedStatus = $('#select-status').val();
            var selectFrom = $('#date-from').val();
            var selectTo = $('#date-to').val();

            approve.AjaxGrid(jId, selectedValue, selectedStatus, selectFrom, selectTo);

            return false;
        });

        $('#select-project').change(function () {
            if ($.fn.DataTable.isDataTable('#tbl-table')) {
                $('#tbl-table').DataTable().clear().destroy();
            }
            var selectedValue = $('#select-project').val();
            var selectedStatus = $('#select-status').val();
            var selectFrom = $('#date-from').val();
            var selectTo = $('#date-to').val();

            approve.AjaxGrid(jId, selectedValue, selectedStatus, selectFrom, selectTo);
            return false;
        });

        $('#select-status').change(function () {
            if ($.fn.DataTable.isDataTable('#tbl-table')) {
                $('#tbl-table').DataTable().clear().destroy();
            }
            var selectedValue = $('#select-project').val();
            var selectedStatus = $('#select-status').val();
            var selectFrom = $('#date-from').val();
            var selectTo = $('#date-to').val();

            approve.AjaxGrid(jId, selectedValue, selectedStatus, selectFrom, selectTo);
            return false;
        });

        $('#date-from').change(function () {
            if ($.fn.DataTable.isDataTable('#tbl-table')) {
                $('#tbl-table').DataTable().clear().destroy();
            }
            var selectedValue = $('#select-project').val();
            var selectedStatus = $('#select-status').val();
            var selectFrom = $('#date-from').val();
            var selectTo = $('#date-to').val();

            approve.AjaxGrid(jId, selectedValue, selectedStatus, selectFrom, selectTo);
            return false;
        });

        $('#date-to').change(function () {
            if ($.fn.DataTable.isDataTable('#tbl-table')) {
                $('#tbl-table').DataTable().clear().destroy();
            }
            var selectedValue = $('#select-project').val();
            var selectedStatus = $('#select-status').val();
            var selectFrom = $('#date-from').val();
            var selectTo = $('#date-to').val();

            approve.AjaxGrid(jId, selectedValue, selectedStatus, selectFrom, selectTo);
            return false;
        });

        
        var selectedValue = $('#select-project').val();
        var selectedStatus = $('#select-status').val();
        var selectFrom = $('#date-from').val();
        var selectTo = $('#date-to').val();
        approve.AjaxGrid(jId, selectedValue, selectedStatus, selectFrom, selectTo); 
     },

    AjaxGrid: function (jId, selectedValue, selectedStatus, selectFrom, selectTo) {
        
        tblUnit = $('#tbl-table').dataTable({
            "dom": '<<t>lip>',
            "processing": true,
            "serverSide": true,
            "ajax": {
                url: baseUrl + 'Approve/JsonAjaxGridTransList',
                type: "POST",
                data: function (json) {                                        
                    var datastring = $("#form-search").serialize();
                    json.JuristicId = jId;
                    json.ProjectId = selectedValue;
                    json.Status = selectedStatus;
                    json.ValidForm = selectFrom;
                    json.ValidThrough = selectTo;
                    json = datastring + '&' + $.param(json);

                    return json;
                },
                "dataSrc": function (res) {
                    //app.ajaxVerifySession(res);                    
                    return res.data;
                },
                complete: function (res) {
                    $("button[data-action='list-ap-detail']").unbind('click').click((e) => {

                        let JuristicId = jId;
                        let TransId = $(e.currentTarget).attr('data-id');
                        
                        //alert("com" + id);
                        //window.location.href = baseUrl + 'Approve/Detail?ID=' + id;
                     
                        // ตัวอย่าง JSON parameter
                        const jsonParam = {
                            JuristicId: JuristicId,
                            ID: TransId,
                        };

                        // แปลง JSON เป็น string
                        const jsonString = JSON.stringify(jsonParam);

                        // เข้ารหัส string ด้วย encodeURIComponent
                        const encodedParam = encodeURIComponent(jsonString);

                        // สร้าง URL ใหม่พร้อมกับพารามิเตอร์ที่เข้ารหัส
                        const newUrl = baseUrl + 'Approve/Detail?data=' + encodedParam;  //`http://example.com/your-endpoint?data=${encodedParam}`;
                        // Redirect ไปยัง URL ใหม่
                        window.location.href = newUrl;

                        return false;
                    });
                    //$("button[data-action='create-quotation']").unbind('click').click((e) => {
                    //    let id = $(e.currentTarget).attr('data-unit-id');
                    //    unit.modalConfirmCreateQuotation(id);
                    //    return false;
                    //});
                    $(document).on('click', "button[data-action='print-report']", function (e) {
                        //e.preventDefault(); // Prevent default action

                        var transId = $(e.currentTarget).attr('data-id');

                        var data = {
                            transId: transId,
                        };
                        $.ajax({
                            url: baseUrl + 'Report/GetPathPDF',
                            type: 'post',
                            dataType: 'json',
                            data: data,
                            success: function (resp) {

                                if (resp.success) {
                                    window.open(baseUrl + resp.data, "_blank");

                                }
                            },
                            error: function (xhr, status, error) {
                                // do something
                                alert(" Coding Error ")
                            },
                        });
                        return false;
                    });

                }
            },
            "ordering": true,
            "order": [[13, "desc"]],
            "columns": [
                {
                    'data': 'ID',
                    'orderable': false,
                    'width': '20px',
                    "className": "text-center",
                    'mRender': function (ID, type, data, obj) {
                                var html = '<button  data-action="list-ap-detail" data-id="' + data.ID + '" class="btn bg-red-lt btn-icon btn-rounded">';
                                html += '<i class="fa-regular fa-file"></i>';
                                html += '</button>';

                                return html;
                    }
                },
                {
                    'data': 'ID',
                    'orderable': false,
                    'width': '20px',
                    "className": "text-center",
                    'mRender': function (ID, type, data, obj) {
                        if (data.Status == 2) {
                            
                            var html = '<button data-action="print-report" data-id ="'+ data.ID + '"class="btn bg-red-lt btn-icon btn-rounded">'
                            html += '<i class="fa-solid fa-print"></i>';
                            html += '</button>';
                            

                            return html;
                        }
                        else {
                            return "";
                        }
                    }
                },
                { 'data': 'OrderNo', "className": "text-center " },
                { 'data': 'ProjectName', "className": "text-center " },
                { 'data': 'AddrNo', "className": "text-center " },
                { 'data': 'CustomerName', "className": "text-center" },
                { 'data': 'StaffName', "className": "text-center" },
                { 'data': 'WorkDate', "className": "text-center" },
                { 'data': 'WorkTime', "className": "text-center" },
                { 'data': 'UsedQuota', "className": "text-center" },
                {
                    'data': 'StatusDesc',
                    "className": "text-center",
                    'mRender': function (data, type, row) {
                        var color;
                        switch (row.Status) {
                            case 1:
                                color = 'orange';
                                break;
                            case 2:
                                color = 'green';
                                break;
                            case 3:
                                color = 'red';
                                break;
                            case 5:
                                color = 'skyblue';
                                break;
                            default:
                                color = 'black'; // Default color if none of the above cases match
                        }

                        // Apply the color using inline CSS
                        return '<span style="color:' + color + ';">' + data + '</span>';
                    }
                },
                { 'data': 'ApproveBy', "className": "text-center" },
                { 'data': 'Note', "className": "text-center" },
                { 'data': 'CreateDate', "className": "text-center" },
                
            ]
        });
    },
}
