﻿define(['knockout',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'bootstrap-dialog',
    'knockout-postbox'
], function (ko, ajaxHelper, disposables, BootstrapDialog) {
        return function Dialog(jobId) {
            var self = this;
            var dialog;

            function addLineCallback(data) {
                ko.postbox.publish("LineEdited", data);
                self.CloseModal();
            }

            self.CloseModal = function () {
                dialog.close();
            };

            self.OpenModal = function () {
                dialog = BootstrapDialog.show({
                    title: 'Edit Line',
                    size: BootstrapDialog.SIZE_WIDE,
                    nl2br: false,
                    type: BootstrapDialog.TYPE_DEFAULT,
                    autospin: true,
                    onshown: function (dialogRef) {
                        getLineDetail().done(function (data, textStatus, jqXHR) {
                            getLineDetailDone(dialogRef, data, textStatus, jqXHR);
                        });
                    },
                    onhide: function (dialogRef) {
                        disposables.dispose("EditLine");
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
                            label: 'Save',
                            cssClass: 'btn-success',
                            icon: 'glyphicon glyphicon-floppy-disk',
                            action: function (dialogRef) {
                                /*Ask To Add New Item*/
                                ko.postbox.publish("EditLine", addLineCallback);
                            }
                        }
                    ]
                });
            };

            function getLineDetail() {
                return ajaxHelper.GetPartialView({ 'id': jobId }, '/Line/_Edit');
            }

            function getLineDetailDone(dialogRef, data, textStatus, jqXHR) {
                dialogRef.setMessage(data);
            }

        }
    });
