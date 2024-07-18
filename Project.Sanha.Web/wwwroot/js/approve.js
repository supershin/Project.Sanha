
const approve = {
    init: (id)=> {
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
            approve.AjaxGrid(id, selectedValue, "");
        });

        $('#select-status').change(function () {
            var selectedStatus = $(this).val();
            approve.AjaxGrid(id, selectedValue, selectedStatus);
        });

        approve.AjaxGrid(id,"",""); 
     },

    AjaxGrid: function (id, selectedValue, selectedStatus) {
        
        tblUnit = $('#tbl-table').dataTable({
            "dom": '<<t>lip>',
            "processing": true,
            "serverSide": true,
            "ajax": {
                url: baseUrl + 'Approve/JsonAjaxGridTransList',
                type: "POST",
                data: function (json) {                                        
                    var datastring = $("#form-search").serialize();
                    json.JuristicId = id;
                    json = datastring + '&' + $.param(json);

                    return json;
                },
                "dataSrc": function (res) {
                    //app.ajaxVerifySession(res);                    
                    return res.data;
                },
                complete: function (res) {
                    $("button[data-action='list-ap-detail']").unbind('click').click((e) => {

                        let id = $(e.currentTarget).attr('data-id');
                        
                        //alert("com" + id);
                        //window.location.href = baseUrl + 'Approve/Detail?ID=' + id;
                     
                        // ตัวอย่าง JSON parameter
                        const jsonParam = {
                            ID: id,
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
                }
            },
            "ordering": true,
            "order": [[11, "desc"]],
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
                        var html = '<button  data-action="list-ap-detail" data-id="' + data.ID + '" class="btn bg-red-lt btn-icon btn-rounded">';
                        html += '<i class="fa-solid fa-print"></i>';
                        html += '</button>';

                        return html;
                    }
                },  
                { 'data': 'ProjectName', "className": "text-center " },
                { 'data': 'AddrNo', "className": "text-center " },
                { 'data': 'CustomerName', "className": "text-center" },
                { 'data': 'StaffName', "className": "text-center" },
                { 'data': 'WorkDate', "className": "text-center" },
                { 'data': 'WorkTime', "className": "text-center" },
                { 'data': 'Quota', "className": "text-center" },
                { 'data': 'UsedQuota', "className": "text-center" },
                { 'data': 'Status', "className": "text-center" },
                { 'data': 'CreateDate', "className": "text-center" },
                
            ]
        });
    },
} 