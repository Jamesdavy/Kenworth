define(['knockout',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'bootstrap-dialog',
    'knockout-postbox'
], function (ko, ajaxHelper, disposables, BootstrapDialog) {
        return function Dialog(contactId) {
            var self = this;
            var dialog;

            function addContactCallback(data) {
                ko.postbox.publish("ContactEdited", data);
                self.CloseModal();
            }

            self.CloseModal = function () {
                dialog.close();
            };

            self.OpenModal = function () {
                dialog = BootstrapDialog.show({
                    title: 'Edit Contact',
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
                        disposables.dispose("EditContact");
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
                                ko.postbox.publish("EditContact", addContactCallback);
                            }
                        }
                    ]
                });
            };

            function getPartial() {
                return ajaxHelper.GetPartialView({ 'id': contactId }, '/Contact/_Edit');
            }

            function getPartialDone(dialogRef, data, textStatus, jqXHR) {
                dialogRef.setMessage(data);
            }

        }
    });
