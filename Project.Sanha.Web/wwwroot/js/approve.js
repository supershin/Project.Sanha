
const approve = {
    init: ()=> {
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
        approve.AjaxGrid();
     },

    AjaxGrid: function () {
        var project_id = "1";
        tblUnit = $('#tbl-table').dataTable({
            "dom": '<<t>lip>',
            "processing": true,
            "serverSide": true,
            "ajax": {
                url: baseUrl + 'Approve/JsonAjaxGridTransList',
                type: "POST",
                data: function (json) {                                        
                    var datastring = $("#form-search").serialize();
                    json.ProjectID = project_id;
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
                            ID: "30",
                            unit_id: "John-Doe"

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
            "order": [[3, "desc"]],
            "columns": [
            {
                    'data': 'ID',
                    'orderable': false,
                    'width': '20px',
                    "className": "text-center",
                    'mRender': function (ID, type, data, obj) {
                        //var html = 'button data-action="list-ap-detail"  data-id="' + data.ID + '" class="btn-icon btn-search"';
                        //html += '<i class="fas fa-file text-primary"></i>data.ID</button > ';
                        //var html = '<button data-action="list-ap-detail"  data-id="' + data.ID + '" class="btn-icon btn-search"';
                       // html += '<i class="fas fa-file text-primary"></i></button> ';
                        console.log("da: " + data.ID);
                        var html = '<button  data-action="list-ap-detail" data-id="' + data.ID + '" class="btn bg-red-lt btn-icon btn-rounded">';
                        html += '<svg xmlns="http://www.w3.org/2000/svg" class="btn-delete icon icon-tabler icon-tabler-trash" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">< path stroke = "none" d = "M0 0h24v24H0z" fill = "none" ></path ><path d="M4 7l16 0"></path><path d="M10 11l0 6"></path><path d="M14 11l0 6"></path><path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12"></path><path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3"></path></svg >';
                        html += '</button>';

                        return html;
                    }
                },
                { 'data': 'unit_id', "className": "text-center" },
                { 'data': 'customer_name', "className": "text-center" },
                { 'data': 'create_date', "className": "text-center" }
                //{
                //    'data': 'ID',
                //    //'orderable': false,
                //    'width': '20px',
                //    "className": "text-center",
                //    'mRender': function (ID, type, data, obj) {
                //        var html = '<div class="d-block mb-1">'
                //        html += '<span style="width:75px;"class="badge ' + data.CssStyle + '" >' + data.UnitStatus + '</span> '
                //        html += '</div>'
                //        //if (data.UnitStatusID == conts_available) {
                //        //    //html += '<button  class="btn btn-sm bg-cyan-lt" data-unit-id=" '+ data.ID + ' " data-action="create-quotation" ><svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-plus" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">< path stroke = "none" d = "M0 0h24v24H0z" fill = "none" ></path ><path d="M12 5l0 14"></path><path d="M5 12l14 0"></path></svg > Create Quotation</button> '
                //        //    html += '<button  class="btn btn-sm bg-green-lt"><svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-plus" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">< path stroke = "none" d = "M0 0h24v24H0z" fill = "none" ></path ><path d="M12 5l0 14"></path><path d="M5 12l14 0"></path></svg > Create Booking</button>'
                //        //}
                //        return html;
                //    }
                //},
                //{
                //    'data': 'LastSaleOrderID',
                //    'orderable': false,
                //    'width': '20px',
                //    "className": "text-center",
                //    'mRender': function (ID, type, data, obj) {
                //        var html = '';
                //        if (data.LastSaleOrderID != null) {
                //            let bg_color = (data.LastStateID == state_contract) ? "bg-green-lt"
                //                : (data.LastStateID == state_booking) ? "bg-orange-lt"
                //                    : (data.LastStateID == state_quotation) ? "bg-cyan-lt" : "";
                //            html = '<div class="d-block mb-1">'
                //            html += '<a href="' + baseUrl + 'SaleOrder/Detail/' + data.LastSaleOrderID + '" style="min-width:120px;width:auto;" class="btn btn-sm ' + bg_color + '" >' + data.LastSaleOrderNumber + '</a>'
                //            html += '</div>'
                //        }
                //        if (data.UnitStatusID == conts_available) {
                //            html += '<button style="width:120px" class="btn btn-sm bg-lime-lt" data-unit-id=" ' + data.ID + ' " data-action="create-quotation" ><svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-plus" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">< path stroke = "none" d = "M0 0h24v24H0z" fill = "none" ></path ><path d="M12 5l0 14"></path><path d="M5 12l14 0"></path></svg > Create Quotation</button>'
                //        }
                //        return html;
                //    }
                //},
                //{ 'data': 'Build', "className": "text-center" },
                //{ 'data': 'Floor', "className": "text-center" },
                //{ 'data': 'Area', "className": "text-center" },
                //{ 'data': 'SellingPrice', "className": "text-center" },
                //{ 'data': 'UpdateDate', "className": "text-center" },
                //{
                //    'data': 'ID',
                //    'orderable': false,
                //    'width': '20px',
                //    "className": "text-center",
                //    'mRender': function (ID, type, data, obj) {
                //        var html = '<a href="' + baseUrl + 'Unit/Detail?ProjectID=' + data.ProjectID + '&ID=' + data.ID + '" class="btn bg-indigo-lt btn-icon btn-rounded">';
                //        html += '<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-device-ipad-search" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">< path stroke = "none" d = "M0 0h24v24H0z" fill = "none" ></path ><path d="M11.5 21h-5.5a2 2 0 0 1 -2 -2v-14a2 2 0 0 1 2 -2h12a2 2 0 0 1 2 2v6"></path><path d="M9 18h2"></path><path d="M18 18m-3 0a3 3 0 1 0 6 0a3 3 0 1 0 -6 0"></path><path d="M20.2 20.2l1.8 1.8"></path></svg ></a > ';
                //        html += '<button  data-action="delete-unit" data-id="' + data.ID + '" class="btn bg-red-lt btn-icon btn-rounded">';
                //        html += '<svg xmlns="http://www.w3.org/2000/svg" class="btn-delete icon icon-tabler icon-tabler-trash" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">< path stroke = "none" d = "M0 0h24v24H0z" fill = "none" ></path ><path d="M4 7l16 0"></path><path d="M10 11l0 6"></path><path d="M14 11l0 6"></path><path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12"></path><path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3"></path></svg >';
                //        html += '</button>';
                //        return html;
                //    }
                //}
            ]
        });
    },
} 