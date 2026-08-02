
$(document).ready(function () {
    twoNoInputmask();
    threeNoInputmask();
    $('input:radio[value="iran"]').prop('checked', true);


    $('input[name="plateType"]').click(function () {
        if ($('input[name="plateType"]:checked').val() == 'iran') {
            iranPlate();
        } else {
            otherPlate();
        }
    })

    $('#save_btn').on('click', async function () {
        if ($('#weight').val() == '' || $('#selectConsignee').val() == '' || $('#tank').val() == '' || $('#commodity').val() == '' || $('#selectTruckType').val() == '' ||
            $('#twoNo').val() == '' || $('#threeNo').val() == '' || $('#provience').val() == '') {
            Swal.fire({
                title: "Error!",
                text: "You may have an unfilled field.",
                icon: "error",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            });
            return;
        }
        const picFileIn = await getFromDB("GateInPic");

        $.ajax({
            url: '/Operation/SaveGateEntrance',
            type: 'POST',
            data: {
                baskool: $('#selectBaskool').val(),
                weight: $('#weight').val(),
                consigneeId: $('#selectConsignee').val(),
                tankTypeId: $('#tank').val(),
                commodityId: $('#commodity').val(),
                trucktypeId: $('#selectTruckType').val(),
                plateType: $('input[name="plateType"]:checked').val(),
                twoNo: $('#twoNo').val(),
                alphabet: $('#alphabet').val(),
                threeNo: $('#threeNo').val(),
                provience: $('#provience').val(),
                otherPlateNo: $('#otherPlateInput').val(),
                customPermission: $('#customPermission').val(),
                gateInFrontPlatePic: picFileIn,
                description: $('#descriptionIn').val(),
            },
            success: async function (res) {
                if (res.state == true) {
                    Swal.fire({
                        title: "Wellcome!",
                        icon: "success",
                        showConfirmButton: false,
                        timer: 2000,
                    });
                    await deleteFromDB("GateInPic");
                    $('#kt_modal_stormBreaker').modal('hide');
                    setTimeout(location.reload.bind(location), 2500);

                } else {
                    if (res.message != '') {
                        Swal.fire({
                            title: "Error!",
                            text: res.message,
                            icon: "error",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                        $('#kt_modal_stormBreaker').modal('hide');
                        document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
                        return;
                    }
                    Swal.fire({
                        title: "Error!",
                        text: "You may have an unfilled field.",
                        icon: "error",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    })
                }
            }
        })
    });

    $('#readBaskool').click(function () {
        emptyDropDowns();
        $('#alphabet').val('ع');
        $.ajax({
            url: '/Operation/GetOilTankGateEnterranceData',
            type: 'GET',
            success: function (res) {
                if (res !== null) {
                    for (let i = 0; i < res.consigneeList.length; i++) {
                        $('#selectConsignee').append('<option value="' + res.consigneeList[i].id + '">' + res.consigneeList[i].consigneeName + '</option>');
                    }
                    for (let i = 0; i < res.commodityList.length; i++) {
                        $('#commodity').append('<option value="' + res.commodityList[i].id + '">' + res.commodityList[i].commodityTypeName + '</option>');
                    }
                    for (let i = 0; i < res.tankTypeList.length; i++) {
                        $('#tank').append('<option value="' + res.tankTypeList[i].id + '">' + res.tankTypeList[i].tankName + '</option>');
                    }
                    for (let i = 0; i < res.truckTypeList.length; i++) {
                        $('#selectTruckType').append('<option value="' + res.truckTypeList[i].id + '">' + res.truckTypeList[i].truckTypeName + '</option>');
                    }
                    $('#selectConsignee,#commodity, #tank, #selectTruckType').select2({
                        dropdownParent: $('#kt_modal_stormBreaker')
                    });
                }
            }
        });

    });

    $('#selectBaskool').change(function () {
        var baskool = $('#selectBaskool').val();
        $('#weight').val('');
        $.ajax({
            url: '/Operation/GetBaskoolData',
            type: 'GET',
            data: { baskool: baskool },
            success: function (res) {
                if (res == null || res == 0) {
                    Swal.fire({
                        title: "No Enter!",
                        text: "There isn`t any Tanker on this Baskool",
                        icon: "error",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    }).then(function () {
                        // $('#kt_modal_stormBreaker').modal('hide');
                        // location.reload();
                    })
                    return;
                } else {
                    $('#weight').val(res);
                }
            }
        });
    });



    $('#camera_btn').click(function () {
        getPlateNoFromCam(1);
    });

})

function twoNoInputmask() {
    Inputmask({
        "mask": "99",
    }).mask(".twoNo");
}
function threeNoInputmask() {
    Inputmask({
        "mask": "999",
    }).mask(".threeNo");
}
function iranPlate() {
    $('.otherWrapper').toggleClass('otherWrapper wrapper');
    $('#otherPlate').css('display', 'none');
    $('.license-plate').css('display', 'flex');
    $('.wrapper').css('height', '194px');
}
function otherPlate() {
    $('.wrapper').toggleClass('wrapper otherWrapper');
    $('#otherPlate').css('display', 'flex');
    $('.license-plate').css('display', 'none');
    $('.otherWrapper').css('height', '20px');
}
function emptyDropDowns() {
    $('#weight').val('');
    $('#selectBaskool').val('');
    $('#selectConsignee').empty();
    $('#selectConsignee').append('<option></option>');
    $('#commodity').empty();
    $('#commodity').append('<option></option>');
    $('#tank').empty();
    $('#tank').append('<option></option>');
    $('#selectTruckType').empty();
    $('#selectTruckType').append('<option></option>');
    $('input:radio[value="iran"]').prop('checked', true);
    iranPlate();
    $('#otherPlateInput').val('');
    $('#twoNo').val('');
    $('#alphabet').val('');
    $('#threeNo').val('');
    $('#provience').val('');
    $('#platePic').attr('src', '');
    $('#plateExit').attr('src', '');
}
function getPlateNoFromCam(cameraId) {
     $.ajax({
            url: '/Operation/GetPlateNoFromCam?cameraId=' + cameraId,
            type: 'GET',
            success: async function (res) {
                if (res.item1 != '') {
                    $('#twoNo').val(String(res.item1));
                    $('#alphabet').val(String(res.item2));
                    $('#threeNo').val(String(res.item3));
                    $('#provience').val(String(res.item4));
                    var image = 'data:image/png;base64,' + res.item5;
                    if (cameraId == 1) {
                        $('#platePic').attr('src', image);
                        saveToDB("GateInPic", res.item5);
                    }
                    if (cameraId == 3) {
                        $('#plateExit').attr('src', image);
                        saveToDB("GateOutPic", res.item5);
                    }
                    return (String(res.item1) + String(res.item2) + String(res.item3) + '-' + String(res.item4));
                } else {
                    return (null);
                }
            }
    })
}

const DB_NAME = "AppDB";
const STORE_NAME = "images";
const DB_VERSION = 1;

async function openDB() {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(DB_NAME, DB_VERSION);

        request.onupgradeneeded = (event) => {
            const db = event.target.result;

            if (!db.objectStoreNames.contains(STORE_NAME)) {
                db.createObjectStore(STORE_NAME);
            }
        };

        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
    });
}

async function saveToDB(key, value) {
    const db = await openDB();

    return new Promise((resolve, reject) => {
        const tx = db.transaction(STORE_NAME, "readwrite");
        const store = tx.objectStore(STORE_NAME);

        const request = store.put(value, key);

        request.onsuccess = () => resolve(true);
        request.onerror = () => reject(request.error);
    });
}

async function deleteFromDB(key) {
    const db = await openDB();

    return new Promise((resolve, reject) => {
        const tx = db.transaction(STORE_NAME, "readwrite");
        const store = tx.objectStore(STORE_NAME);

        const request = store.delete(key);

        request.onsuccess = () => resolve(true);
        request.onerror = () => reject(request.error);
    });
}

async function getFromDB(key) {
    const db = await openDB();

    return new Promise((resolve, reject) => {
        const tx = db.transaction(STORE_NAME, "readonly");
        const store = tx.objectStore(STORE_NAME);

        const request = store.get(key);

        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
    });
}
