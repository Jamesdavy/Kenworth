define(['knockout',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'bootstrap-dialog',
    'knockout-postbox'
], function (ko, ajaxHelper, disposables, BootstrapDialog) {
        return function Dialog(lineId) {
            var self = this;
            var dialog;

            function addTimesheetCallback(data) {
                ko.postbox.publish("TimesheetAdded", data);
                self.CloseModal();
            }

            function saveAndAddTimesheetCallback(data) {
                ko.postbox.publish("TimesheetAdded", data);
            }

            self.CloseModal = function () {
                dialog.close();
            };

            self.OpenModal = function () {
                dialog = BootstrapDialog.show({
                    title: 'Add Timesheet',
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
                        disposables.dispose("AddTimesheet");
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
                                ko.postbox.publish("AddTimesheet", addTimesheetCallback);
                            }
                        },
                        {
                            label: 'Save & Add',
                            cssClass: 'btn-success',
                            icon: 'glyphicon glyphicon-floppy-disk',
                            action: function (dialogRef) {
                                /*Ask To Add New Item*/
                                ko.postbox.publish("AddBOM", saveAndAddTimesheetCallback);
                            }
                        }
                    ]
                });
            };

            function getPartial() {
                return ajaxHelper.GetPartialView({ 'id': lineId }, '/Timesheet/_Create');
            }

            function getPartialDone(dialogRef, data, textStatus, jqXHR) {
                dialogRef.setMessage(data);
            }

        }
    });
