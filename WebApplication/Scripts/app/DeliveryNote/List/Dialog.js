define(['knockout',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'bootstrap-dialog',
    'knockout-postbox'
], function (ko, ajaxHelper, disposables, BootstrapDialog) {
        return function Dialog(jobId) {
            var self = this;
            var dialog;

            function createDeliveryNoteCallback(data) {
                ko.postbox.publish("DeliveryNoteCreated", data);
                self.CloseModal();
            }

            self.CloseModal = function () {
                dialog.close();
            };

            self.OpenModal = function () {
                dialog = BootstrapDialog.show({
                    title: 'Delivery Notes',
                    size: BootstrapDialog.SIZE_WIDE,
                    nl2br: false,
                    type: BootstrapDialog.TYPE_DEFAULT,
                    autospin: true,
                    onshown: function (dialogRef) {
                        getPartial().done(function (data, textStatus, jqXHR) {
                            getPartialDone(dialogRef, data, textStatus, jqXHR);
                        });
                    },
                    onhide: function (dialogRef) {
                        disposables.dispose("CreateDeliveyNote");
                    },
                    buttons: [
                        {
                            label: 'Close',
                            icon: 'glyphicon glyphicon-remove',
                            action: function (dialogRef) {
                                dialogRef.close();
                            }
                        },
                        {
                            label: 'Create Delivery Note',
                            cssClass: 'btn-success',
                            icon: 'glyphicon glyphicon-floppy-disk',
                            action: function (dialogRef) {
                                /*Ask To Add New Item*/
                                ko.postbox.publish("CreateDeliveyNote", createDeliveryNoteCallback);
                            }
                        }
                    ]
                });
            };

            function getPartial() {
                return ajaxHelper.GetPartialView({ 'id': jobId }, '/DeliveryNote/_List');
            }

            function getPartialDone(dialogRef, data, textStatus, jqXHR) {
                dialogRef.setMessage(data);
            }

        }
    });
