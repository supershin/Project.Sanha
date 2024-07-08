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
            return false;
        });
        $("#btn-save-sign-jm").click(() => {
            $('#modal-sign-jm').modal('hide');
            return false;
        });
        $("#btn-save-unit-equipment").click(() => {
            unitEquipment.saveUnitEquipmentSign();
            return false;
        });
    },
    initSignaturePad: () => {
        $('#modal-sign').on('shown.bs.modal', function (e) {
            console.log("click pad 1");
            let canvas = $("#signature-pad canvas");
            let parentWidth = $(canvas).parent().outerWidth();
            let parentHeight = $(canvas).parent().outerHeight();
            canvas.attr("width", parentWidth + 'px')
                .attr("height", parentHeight + 'px');
            signaturePad = new SignaturePad(canvas[0], {
                backgroundColor: 'rgb(255, 255, 255)'
            });
        });
       
        $(document).on('click', '#modal-sign .clear', function () {
            signaturePad.clear();
        });

    },
    initSignaturePad_JM: () => {
        $('#modal-sign-jm').on('shown.bs.modal', function (e) {
            let canvas = $("#signature-pad-jm canvas");
            let parentWidth = $(canvas).parent().outerWidth();
            let parentHeight = $(canvas).parent().outerHeight();
            canvas.attr("width", parentWidth + 'px')
                .attr("height", parentHeight + 'px');
            signaturePad_JM = new SignaturePad(canvas[0], {
                backgroundColor: 'rgb(255, 255, 255)'
            });
            console.log("click pad 2")
        });
        $(document).on('click', '#modal-sign-jm .clear', function () {            
            signaturePad_JM.clear();
        });
    },
    saveUnitEquipmentSign: () => {
        var data = {
            ProjectID: projectID,
            UnitID: unitID,
            UnitCode: unitCode,
            DocumentTypeID: documentTypeID,
            Sign: unitEquipment.getSignatureData(),
            SignJM: unitEquipment.getSignatureDataJM()
        };
        Application.LoadWait(true);
        $.ajax({
            url: baseUrl + 'Document/SaveUnitEquipmentSign',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (res) {
                if (res.success) {                    
                    Application.PNotify(res.message, "success");                    
                    window.location.reload();
                }
                else {
                    Application.PNotify(res.message, "error");
                }
                Application.LoadWait(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: JSON.stringify(data)
        });
        return false;
    },
    getSignatureData: () => {
        let dataURL;
        let contentType;
        let storage;
        if (!signaturePad.isEmpty()) {
            dataURL = signaturePad.toDataURL();
            var parts = dataURL.split(';base64,');
            contentType = parts[0].split(":")[1];
            storage = parts[1];
        }
        return {
            MimeType: contentType,
            StorageBase64: storage
        };
    },
    getSignatureDataJM: () => {
        let dataURL;
        let contentType;
        let storage;
        if (!signaturePad_JM.isEmpty()) {
            dataURL = signaturePad_JM.toDataURL();
            var parts = dataURL.split(';base64,');
            contentType = parts[0].split(":")[1];
            storage = parts[1];
        }
        return {
            MimeType: contentType,
            StorageBase64: storage
        };
    }
}