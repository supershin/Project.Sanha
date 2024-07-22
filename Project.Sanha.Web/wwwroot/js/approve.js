
const approve = {
    init: (jId)=> {
        //$("button[data-action='list-ap-detail']").unbind('click').click((e) => {
        //    //alert(baseUrl);
        //    alert("func");
        //    let id = $(e.currentTarget).attr('data-id');
        //    alert(id);
        //    window.location.href = baseUrl + 'Approve/Detail';
        //});
        $('#btnLoad').unbind('click').click((e) => {
            alert("a");
        });

        $('#select-project').change(function () {
            var selectedValue = $(this).val();
            approve.AjaxGrid(jId, selectedValue, "");
        });

        $('#select-status').change(function () {
            var selectedStatus = $(this).val();
            approve.AjaxGrid(jId, selectedValue, selectedStatus);
        });

        approve.AjaxGrid(jId,"",""); 
     },

    AjaxGrid: function (jId, selectedValue, selectedStatus) {
        
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
