let signaturePad;
let signaturePad_JM;

var unitEquipment = {
    init: () => {
        unitEquipment.initSignaturePad();
        unitEquipment.initSignaturePad_JM();
        
        $("#btn-sign").click(() => {
            $('#modal-sign').modal();
            return false;
        });
        $("#btn-sign-jm").click(() => {
            $('#modal-sign-jm').modal();
            return false;
        });
        $("#btn-save-sign").click(() => {
            $('#modal-sign').modal('hide');
            if (!unitEquipment.getSignatureData()) {
                $('#success-icon').hide();
            } else {
                $('#success-icon').show();
            }
            return false;
        });
        $("#btn-save-sign-jm").click(() => {
            $('#modal-sign-jm').modal('hide');
            if (!unitEquipment.getSignatureDataJM()) {
                $('#success-icon-jm').hide();
            } else {
                $('#success-icon-jm').show();
            }
            return false;
        });
        $("#btn-cancel-form").click(() => {
            unitEquipment.cancelForm();
            $('.loading').show();
            return false;
        });
        $("#btn-save-unit-equipment").click(() => {
            var usedQuota = $("#UsingQuota").val();
            var quota = $("#Quota").val();
            
            var message = "";

            if (usedQuota <= 0 || usedQuota > quota || usedQuota === null || isNaN(usedQuota)) {
                message += "กรุณาตรวจสอบจำนวนสิทธิ์คงเหลือ<br>";
            }
            else if ($("#CustomerName").val() === null || $("#CustomerName").val().trim() === "") {
                message += "กรุณาตรวจสอบชื่อของลูกค้า<br>";
            }
            else if ($("#RelationShip").val() === null || $("#RelationShip").val().trim() === "") {
                message += "กรุณาตรวจสอบความสัมพันธ์ลูกค้า<br>";
            }
            else if ($("#CustomerMobile").val() === null || $("#CustomerMobile").val().trim() === "") {
                message += "กรุณาตรวจสอบเบอร์โทรศัพท์ลูกค้า<br>";
            }
            else if ($("#StaffName").val() === null || $("#StaffName").val().trim() === "") {
                message += "กรุณาตรวจสอบชื่อพนักงาน<br>";
            }
            else if (!unitEquipment.getSignatureData() || !unitEquipment.getSignatureDataJM()) {
                message += "กรุณาตรวจสอบลายเซ็นต์<br>";
            }
            else {
                var imageInput = $("#Images")[0];
                var fileCount = imageInput.files.length;

                if (fileCount === 0) {
                    message += "กรุณาตรวจสอบจำนวนรูปภาพที่อัปโหลด<br>";
                }
            }

            if (message !== "") {
                $('#errorModalMessage').html(message);
                $('#errorModal').modal('show');
                return false;
            }

            unitEquipment.saveUnitEquipmentSign();
            $('.loading').show();
            return false;
        });
    },
    initSignaturePad: () => {
        $('#modal-sign').on('shown.bs.modal', function (e) {
            if (signaturePad == null) {
                let canvas = $("#signature-pad canvas");
                let parentWidth = $(canvas).parent().outerWidth();
                let parentHeight = $(canvas).parent().outerHeight();
                canvas.attr("width", parentWidth + 'px')
                    .attr("height", parentHeight + 'px');
                signaturePad = new SignaturePad(canvas[0], {
                    backgroundColor: 'rgb(255, 255, 255)'
                });
            }
        });
       
        $(document).on('click', '#modal-sign .clear', function () {
            signaturePad.clear();
            $('#success-icon').hide();
        });

    },
    initSignaturePad_JM: () => {
        $('#modal-sign-jm').on('shown.bs.modal', function (e) {
            if (signaturePad_JM == null) {
                let canvas = $("#signature-pad-jm canvas");
                let parentWidth = $(canvas).parent().outerWidth();
                let parentHeight = $(canvas).parent().outerHeight();
                canvas.attr("width", parentWidth + 'px')
                    .attr("height", parentHeight + 'px');
                signaturePad_JM = new SignaturePad(canvas[0], {
                    backgroundColor: 'rgb(255, 255, 255)'
                });
            }
        });
        $(document).on('click', '#modal-sign-jm .clear', function () {            
            signaturePad_JM.clear();
            $('#success-icon-jm').hide();
        });
    },
    saveUnitEquipmentSign: () => {
        var formData = new FormData();

        formData.append('UnitShopId', $("#UnitShopId").val());
        formData.append('ProjectId', $("#ProjectId").val());
        formData.append('UnitId', $("#UnitId").val());
        formData.append('ShopId', $("#ShopId").val());
        formData.append('CustomerName', $("#CustomerName").val());
        formData.append('RelationShip', $("#RelationShip").val());
        formData.append('CustomerMobile', $("#CustomerMobile").val());
        formData.append('CustomerEmail', $("#CustomerEmail").val());
        formData.append('StaffName', $("#StaffName").val());
        formData.append('UsingQuota', $("#UsingQuota").val());
        formData.append('Date', $("#Date").val());
        formData.append('StartTime', $("#StartTime").val());
        formData.append('EndTime', $("#EndTime").val());
        formData.append('Remark', $("#Remark").val());
        formData.append('Sign', unitEquipment.getSignatureData());
        formData.append('SignJM', unitEquipment.getSignatureDataJM());

        var files = document.getElementById('Images').files;
        for (var i = 0; i < files.length; i++) {
            formData.append('Images', files[i]);
        }
        //console.log(formData);
        //Application.LoadWait(true);
        $.ajax({
            url: baseUrl + 'Information/SaveUnitEquipmentSign',
            type: 'post',
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (resp) {
                if (resp.success) {
                    $('.loading').hide();

                    let param = btoa(resp.data.ProjectId + ':' + resp.data.UnitId + ':' + resp.data.ContractNo)

                    window.location.href = baseUrl + 'Information?param=' + param;
                }
                else {
                    $('.loading').hide();
                    $('#errorModalMessage').text(resp.message);
                    $('#errorModal').modal('show');
                }
            },
            error: function (xhr, status, error) {
                $('#errorModal').modal('show');
            },
            data: formData
        });
        return false;
    },
    getSignatureData: () => {
        let dataURL;
        let contentType;
        let storage;
        if (signaturePad && !signaturePad.isEmpty()) {
            dataURL = signaturePad.toDataURL();
            var parts = dataURL.split(';base64,');
            contentType = parts[0].split(":")[1];
            storage = parts[1];
        }
        return storage;
    },
    getSignatureDataJM: () => {
        let dataURL;
        let contentType;
        let storage;
        if (signaturePad_JM && !signaturePad_JM.isEmpty()) {
            dataURL = signaturePad_JM.toDataURL();
            var parts = dataURL.split(';base64,');
            contentType = parts[0].split(":")[1];
            storage = parts[1];
        }
        return storage
    },
    validateUsingQuota: () => {
        $('#UsingQuota').on('input', function () {
            var value = parseInt($(this).val(), 10);
            var maxQuota = parseInt($('#Quota').val(), 10);
            var errorMessage = $('#error-message');

            if (value < 1 || value > maxQuota || isNaN(value)) {
                errorMessage.show();
            } else {
                errorMessage.hide();
            }
        });
    },
    cancelForm: () => {
        var data = {
            UnitId: $("#UnitId").val(),
        };
        $.ajax({
            url: baseUrl + 'Information/Cancel',
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                if (resp.success) {
                    $('.loading').hide();

                    let param = btoa(resp.data.ProjectId + ':' + resp.data.UnitId + ':' + resp.data.ContractNo)

                    window.location.href = baseUrl + 'Information?param=' + param;
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