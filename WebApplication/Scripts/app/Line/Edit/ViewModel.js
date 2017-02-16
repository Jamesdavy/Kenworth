define(['knockout',
    'app/BillOfMaterials/Create/Dialog',
    'app/Timesheet/Create/Dialog',
    'app/Line/Edit/BillOfMaterialsModel',
    'app/Line/Edit/TimesheetModel',
    'app/Line/Edit/FileModel',
    'app/BillOfMaterials/Edit/Dialog',
    'app/Timesheet/Edit/Dialog',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'app/Common/UIBlocker',
    'app/Common/Notification',
    'bootstrap-dialog',
    'knockout.validation',
    'knockout-postbox',
    'knockout-kendo'
    //'Common/Validation/globalFloat'
], function (ko, bomDialog,
        timesheetDialog, bomModel, timesheetModel, fileModel, editBomDialog,
        editTimesheetDialog, ajaxHelper, disposables, uiBlocker, notification, BootstrapDialog) {
    return function ViewModel(lineDetail) {
        var self = this;

        var blocker = new uiBlocker();
        var notifier = new notification();

        self.LineId = ko.observable(lineDetail.LineId);
        self.UniqueId = lineDetail.UniqueId;
        self.JobLineId = lineDetail.JobLineId;
        self.Description = ko.observable(lineDetail.Description).extend({ required: true });
        self.Quantity = ko.observable(lineDetail.Quantity).extend({ required: true });
        self.UnitPrice = ko.observable(lineDetail.UnitPrice).extend({ required: true });
        self.ExpectedDeliveryDate = ko.observable(lineDetail.ExpectedDeliveryDateString).extend({ required: true });
        self.DeliveryComments = ko.observable(lineDetail.DeliveryComments);
        self.DrawingNumber = ko.observable(lineDetail.DrawingNumber);

        self.EstimatedHours = ko.observable(lineDetail.EstimatedHours).extend({ required: true });
        self.EstimatedHourlyRate = ko.observable(lineDetail.EstimatedHourlyRate).extend({ required: true });
        self.DBCalculatedUnitPrice = ko.observable(lineDetail.CalculatedUnitPrice).extend({ required: true });;
        self.CustomerRef = ko.observable(lineDetail.tblJobCustomerRef);
        self.LegacyQuote = ko.observable(lineDetail.LegacyQuote);

        self._Status = ko.observable(lineDetail.tblStatusName);

        self.File = ko.observable();
        self.FileObjectURL = ko.observable();
        self.FileBinary = ko.observable();
        self.FileType = ko.observable();

        //self.FileName = ko.observable(lineDetail.tblFileFileName);
        //self.ContentType = ko.observable(lineDetail.tblFileContentType);
        //self.FilePath = ko.observable(lineDetail.FilePath);

        self.FileName = ko.computed(function () {
            var file = this.File();
            return file ? file.name : "";// file.name;
        }, self);

        self.FileSize = ko.computed(function() {
            var file = this.File();
            return file ? file.size : 0;
        }, self);

        self.FileSelected = ko.computed(function () {
            var file = this.File();
            return typeof file !== 'undefined';
        }, self);

        //self.ShowFile = ko.computed(function () {
        //    var fileName = self.FileName();
        //    if (fileName == null)
        //        return false;
        //    return true;
        //}, self);


        self._BillOfMaterials = ko.observableArray([]);

        if (lineDetail.tblPurchaseOrders) {
            var mappedBOM = $.map(lineDetail.tblPurchaseOrders, function (item) { return new bomModel(item); });
            self._BillOfMaterials(mappedBOM);
        }

        self._Timesheets = ko.observableArray([]);

        if (lineDetail.tblTimesheets) {
            var mappedTimesheets = $.map(lineDetail.tblTimesheets, function (item) { return new timesheetModel(item); });
            self._Timesheets(mappedTimesheets);
        }

        self._Files = ko.observableArray([]);

        if (lineDetail.tblFiles) {
            var mappedFiles = $.map(lineDetail.tblFiles, function (item) { return new fileModel(item); });
            self._Files(mappedFiles);
        }

        self.BOMTotal = ko.computed(function() {
            var total = 0;
            for (var j = 0; j < self._BillOfMaterials().length; j++) {
                total = total + (self._BillOfMaterials()[j].Total());
            }
            return total;
        }, self);

        self.LabourTotal = ko.computed(function () {
            if (self.LegacyQuote())
                return 0;
            var total = ((parseFloat(self.EstimatedHours()) * parseFloat(self.EstimatedHourlyRate())) + parseFloat(self.UnitPrice())) * parseFloat(self.Quantity());
            return total;
        }, self);

        self.QuoteTotal = ko.computed(function () {
            if (self.LegacyQuote())
                return 0;
            var total = self.LabourTotal() + self.BOMTotal();
            return total;
        }, self);

        self.CalculatedUnitPrice = ko.computed(function () {
            if (self.LegacyQuote())
                return self.UnitPrice();

            var total = self.QuoteTotal() / self.Quantity();
            return total;
        }, self);


        self.ShowLabour = ko.computed(function () {
            if (self.LegacyQuote())
                return false;
            return true;
        }, self);


        function ChangeStatus(lineId, status) {
            blocker.Show();
            ajaxHelper.Post(JSON.stringify({ 'LineId': lineId, 'Status': status }), '/Line/_ChangeStatus')
                .done(function (response) {
                    notifier.Notify('Saved');
                    ko.postbox.publish("LineStatusChanged", response);
                    self._Status(response.Status);
                }).always(function () {
                    blocker.Hide();
                });
        }

        self.Job = function () {
            ChangeStatus(self.LineId(), 4);
        }

        self.Quote = function () {
            ChangeStatus(self.LineId(), 2);
        }

        self.Dead = function () {
            ChangeStatus(self.LineId(), 1);
        }

        self.Complete = function () {
            ChangeStatus(self.LineId(), 8);
        }

        self.AddBOM = function () {
            var dialog = new bomDialog(self.LineId());
            dialog.OpenModal();
        }

        self.AddTimesheet = function () {
            var dialog = new timesheetDialog(self.LineId());
            dialog.OpenModal();
        }

        self.EditBOM = function (bom) {
            var dialog = new editBomDialog(bom.BillOfMaterialsID());
            dialog.OpenModal();
        }

        self.EditTimesheet = function (timesheet) {
            var dialog = new editTimesheetDialog(timesheet.TimesheetID());
            dialog.OpenModal();
        }

        
        /*Recieve External Request to Add new Item*/
        var editLineSubscription = ko.postbox.subscribe("EditLine", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/Line/_Edit').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                //self.FileName(response.FileName);
                //self.ContentType(response.ContentType);
                //self.FilePath(response.FilePath);

                if (callback)
                    callback(response);


            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("EditLine", editLineSubscription);

        ko.postbox.subscribe("BOMEdited", function(response) {
            var billOfMaterials = self.getBillOfMaterials(response.BillOfMaterialsId);
            if (billOfMaterials) {
                billOfMaterials.Description(response.Description);
                billOfMaterials.Cost(response.Cost);
                billOfMaterials.Quantity(response.Quantity);
                billOfMaterials.Comments(response.Comments);
                billOfMaterials.PurchaseOrderDate(response.PurchaseOrderDateString);
                billOfMaterials.SupplierRef(response.SupplierRef);
                //self.UnitPrice(response.CalculatedUnitPrice);
            }

        });

        ko.postbox.subscribe("TimesheetEdited", function(response) {
            var timesheet = self.getTimesheet(response.TimesheetId);
            if (timesheet) {
                timesheet.Comments(response.Comments);
                timesheet.Hours(response.Hours);
                timesheet.HourlyRate(response.HourlyRate);
                timesheet.TimesheetDate(response.TimesheetDateString);
            }
        });

        ko.postbox.subscribe("BOMAdded", function (response) {
            self._BillOfMaterials.push(new bomModel({
                PurchaseOrderID: response.PurchaseOrderID,
                Description: response.Description,
                Cost: response.Cost,
                Quantity: response.Quantity,
                Comments: response.Comments,
                PurchaseOrderDateString: response.PurchaseOrderDateString,
                SupplierRef: response.SupplierRef
            }));
            //self.UnitPrice(response.CalculatedUnitPrice);
        });

        ko.postbox.subscribe("TimesheetAdded", function (response) {
            self._Timesheets.push(new timesheetModel({
                TimesheetID: response.TimesheetID,
                Comments: response.Comments,
                Hours: response.Hours,
                HourlyRate: response.HourlyRate,
                TimesheetDateString: response.TimesheetDateString,
                OperativeName: response.OperativeName
            }));
        });

        self.DeleteBOM = function (bom) {
            BootstrapDialog.confirm({
                title: 'Warning',
                message: 'Are you Sure?',
                type: BootstrapDialog.TYPE_DANGER,
                btnCancelLabel: 'Cancel',
                btnOKLabel: 'Delete',
                btnOKClass: 'btn-danger',
                callback: function (result) {
                    if (result) {
                        blocker.Show();
                        ajaxHelper.Delete(JSON.stringify({ 'BillOfMaterialsId': bom.BillOfMaterialsID() }), '/BillOfMaterials/_Delete')
                            .done(function (response) {
                                notifier.Notify('Deleted');
                                ko.postbox.publish("BOMDeleted", response);
                                self._BillOfMaterials.remove(bom);
                                
                        }).always(function () {
                                blocker.Hide();
                            });
                    } else {

                    }
                }
            });
        };

        self.DeleteTimesheet = function (timesheet) {
            BootstrapDialog.confirm({
                title: 'Warning',
                message: 'Are you Sure?',
                type: BootstrapDialog.TYPE_DANGER,
                btnCancelLabel: 'Cancel',
                btnOKLabel: 'Delete',
                btnOKClass: 'btn-danger',
                callback: function (result) {
                    if (result) {
                        blocker.Show();
                        ajaxHelper.Delete(JSON.stringify({ 'TimesheetId': timesheet.TimesheetID() }), '/Timesheet/_Delete')
                            .done(function (response) {
                                notifier.Notify('Deleted');
                                ko.postbox.publish("TimesheetDeleted", response);
                                self._Timesheets.remove(timesheet);
                            }).always(function () {
                                blocker.Hide();
                            });
                    } else {

                    }
                }
            });
        };

        ko.postbox.subscribe("FileUploaded", function (response) {
            self._Files.push(new fileModel({
                LineID : response.LineID,
                FileID : response.FileID,
                FileName : response.FileName,
                FilePath : response.FilePath,
                ContentType : response.ContentType,
                Name : response.Name
            }));
        });

        self.UploadFile = function() {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            ajaxHelper.Post(data, '/Line/_UploadFile')
                .done(function (response) {
                    notifier.Notify('Saved');
                    ko.postbox.publish("FileUploaded", response);
                    
                }).always(function () {
                    blocker.Hide();
                });
        }

        self.DeleteFile = function (file) {
            BootstrapDialog.confirm({
                title: 'Warning',
                message: 'Are you Sure?',
                type: BootstrapDialog.TYPE_DANGER,
                btnCancelLabel: 'Cancel',
                btnOKLabel: 'Delete',
                btnOKClass: 'btn-danger',
                callback: function (result) {
                    if (result) {
                        blocker.Show();
                        ajaxHelper.Delete(JSON.stringify({ 'LineId': self.LineId(), 'FileId': file.FileID() }), '/Line/_DeleteFile')
                            .done(function (response) {
                                notifier.Notify('Saved');
                                ko.postbox.publish("FileDeleted", response);
                                self._Files.remove(file);
                            }).always(function () {
                                blocker.Hide();
                            });
                    } else {

                    }
                }
            });
        };


        self.getBillOfMaterials = function (billOfMaterialsId) {
            for (var i = 0; i < self._BillOfMaterials().length; i++) {
                if (self._BillOfMaterials()[i].BillOfMaterialsID() == billOfMaterialsId) {
                    return self._BillOfMaterials()[i];
                }
            }
        };

        self.getTimesheet = function (timesheetId) {
            for (var i = 0; i < self._Timesheets().length; i++) {
                if (self._Timesheets()[i].TimesheetID() == timesheetId) {
                    return self._Timesheets()[i];
                }
            }
        };

        self.getFile = function (fileId) {
            for (var i = 0; i < self._Files().length; i++) {
                if (self._Files()[i].FilesID() == fileId) {
                    return self._Files()[i];
                }
            }
        };

    }
});