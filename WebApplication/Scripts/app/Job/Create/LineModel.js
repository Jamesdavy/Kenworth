define([
    'knockout',
    'app/Common/AjaxHelper',
    'app/Common/UIBlocker',
    'app/Common/Notification',
    'app/Job/Create/BillOfMaterialsModel',
    'app/Job/Create/TimesheetModel',
    'bootstrap-dialog'
], function (ko, ajaxHelper, uiBlocker, notification, bomModel, timesheetModel, BootstrapDialog) {
    return function LineModel(model) {
        var self = this;

        var blocker = new uiBlocker();
        var notifier = new notification();

        self.LineID = ko.observable(model.LineID);
        self.Description = ko.observable(model.Description);
        self.JobLineID = ko.observable(model.JobLineID);
        self.Quantity = ko.observable(model.Quantity);
        self.UnitPrice = ko.observable(model.CalculatedUnitPrice);
        self.ExpectedDeliveryDate = ko.observable(model.ExpectedDeliveryDateString);
        self.DeliveryComments = ko.observable(model.DeliveryComments);
        self.DrawingNumber = ko.observable(model.DrawingNumber);

        self.FileName = ko.observable(model.tblFileFileName);
        self.ContentType = ko.observable(model.tblFileContentType);
        self.FilePath = ko.observable(model.FilePath);
        self._Status = ko.observable(model.tblStatusName);

        self.ShowFile = ko.computed(function () {
            var fileName = self.FileName();
            if (fileName == null || fileName == '')
                return false;
            return true;
        }, self);

        self._BillOfMaterials = ko.observableArray([]);

        if (model.tblPurchaseOrders) {
            var mappedBOM = $.map(model.tblPurchaseOrders, function(item) { return new bomModel(item); });
            self._BillOfMaterials(mappedBOM);
        }

        self._Timesheets = ko.observableArray([]);

        if (model.tblTimesheets) {
            var mappedTimesheets = $.map(model.tblTimesheets, function(item) { return new timesheetModel(item); });
            self._Timesheets(mappedTimesheets);
        }

        self.LineTotal = ko.computed(function () {
            return self.Quantity() * self.UnitPrice();
        }, self);

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

        self.DeleteBOM = function(bom) {
            BootstrapDialog.confirm({
                title: 'Warning',
                message: 'Are you Sure?',
                type: BootstrapDialog.TYPE_DANGER,
                btnCancelLabel: 'Cancel',
                btnOKLabel: 'Delete',
                btnOKClass: 'btn-danger',
                callback: function(result) {
                    if (result) {
                        blocker.Show();
                        ajaxHelper.Delete(JSON.stringify({ 'BillOfMaterialsId': bom.BillOfMaterialsID() }), '/BillOfMaterials/_Delete')
                            .done(function(response) {
                                notifier.Notify('Deleted');
                                self._BillOfMaterials.remove(bom);
                                self.UnitPrice(response.CalculatedUnitPrice);
                            }).always(function() {
                                blocker.Hide();
                            });
                    } else {

                    }
                }
            });
        };

        self.DeleteTimesheet = function(timesheet) {
            BootstrapDialog.confirm({
                title: 'Warning',
                message: 'Are you Sure?',
                type: BootstrapDialog.TYPE_DANGER,
                btnCancelLabel: 'Cancel',
                btnOKLabel: 'Delete',
                btnOKClass: 'btn-danger',
                callback: function(result) {
                    if (result) {
                        blocker.Show();
                        ajaxHelper.Delete(JSON.stringify({ 'TimesheetId': timesheet.TimesheetID() }), '/Timesheet/_Delete')
                            .done(function() {
                                notifier.Notify('Deleted');
                                self._Timesheets.remove(timesheet);
                            }).always(function() {
                                blocker.Hide();
                            });
                    } else {

                    }
                }
            });
        };

        
    }
});