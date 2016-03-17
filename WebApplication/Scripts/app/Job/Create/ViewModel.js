define(['knockout',
        'app/Common/AjaxHelper',
        'app/Job/Create/LineModel',
        'app/Job/Create/BillOfMaterialsModel',
        'app/Job/Create/TimesheetModel',
        'app/Common/UIBlocker',
        'app/Common/Notification',
        'app/Line/Create/Dialog',
        'app/Line/Edit/Dialog',
        'app/BillOfMaterials/Create/Dialog',
        'app/Timesheet/Create/Dialog',
        'app/BillOfMaterials/Edit/Dialog',
        'app/Timesheet/Edit/Dialog',
        'app/Contact/Create/Dialog',
        'app/Contact/Edit/Dialog',
        'app/Contact/List/Dialog',
        'app/DeliveryNote/List/Dialog',
        'bootstrap-dialog',
        'knockout.validation'//,
],
    function (ko,
        ajaxHelper,
        lineModel,
        bomModel,
        timesheetModel,
        uiBlocker,
        notification,
        lineDialog,
        editLineDialog,
        bomDialog,
        timesheetDialog,
        editBomDialog,
        editTimesheetDialog,
        contactDialog,
        editContactDialog,
        listContactDialog,
        listDeliveryNoteDialog,
        BootstrapDialog
        ) {
        return function ViewModel(model) {
            var self = this;

            var blocker = new uiBlocker();
            var notifier = new notification();

            self.Id = model.JobID;
            self.Client = ko.observable(model.tblClientClientCompanyName).extend({ required: true });
            self.ClientId = ko.observable(model.ClientID);
            self.Contact = ko.observable(model.ContactName).extend({ required: true });
            self.ContactId = ko.observable(model.ContactID);
            self.Description = ko.observable(model.Description).extend({ required: true });
            self.ExpectedDeliveryDate = ko.observable(model.ExpectedDeliveryDateString);
            self.CustomerRef = ko.observable(model.CustomerRef);
            self.Comments = ko.observable(model.Comments).extend({ required: true });
            self._Status = ko.observable(model.tblStatusName);

            self._Lines = ko.observableArray([]);
            var mappedLines = $.map(model.tblLines, function (item) { return new lineModel(item); });
            self._Lines(mappedLines);

            self.errors = ko.observableArray([]);

            self.Total = ko.computed(function () {
                var total = 0;
                for (var i = 0; i < self._Lines().length; i++) {
                    total = total + self._Lines()[i].LineTotal();
                }
                return total;
            }, self);

            self.JobTotal = ko.computed(function () {
                var total = 0;
                for (var i = 0; i < self._Lines().length; i++) {
                    if (self._Lines()[i]._Status() == 'Job')
                        total = total + self._Lines()[i].LineTotal();
                }
                return total;
            }, self);

            self.QuoteTotal = ko.computed(function () {
                var total = 0;
                for (var i = 0; i < self._Lines().length; i++) {
                    if (self._Lines()[i]._Status() == 'Quote')
                        total = total + self._Lines()[i].LineTotal();
                }
                return total;
            }, self);

            self.CompleteTotal = ko.computed(function () {
                var total = 0;
                for (var i = 0; i < self._Lines().length; i++) {
                    if (self._Lines()[i]._Status() == 'Complete')
                        total = total + self._Lines()[i].LineTotal();
                }
                return total;
            }, self);

            self.DeadTotal = ko.computed(function () {
                var total = 0;
                for (var i = 0; i < self._Lines().length; i++) {
                    if (self._Lines()[i]._Status() == 'Dead')
                        total = total + self._Lines()[i].LineTotal();
                }
                return total;
            }, self);

            self.TimesheetTotal = ko.computed(function () {
                var total = 0;
                for (var i = 0; i < self._Lines().length; i++) {
                    for (var j = 0; j < self._Lines()[i]._Timesheets().length; j++) {
                        total = total + (self._Lines()[i]._Timesheets()[j].Total());
                    }
                }
                return total;
            }, self);

            self.BOMTotal = ko.computed(function () {
                var total = 0;
                for (var i = 0; i < self._Lines().length; i++) {
                    for (var j = 0; j < self._Lines()[i]._BillOfMaterials().length; j++) {
                        total = total + (self._Lines()[i]._BillOfMaterials()[j].Total());
                    }
                }
                return total;
            }, self);

            self.Costs = ko.computed(function () {
                return self.TimesheetTotal(); // + self.BOMTotal();
            }, self);

            self.ProfitLoss = ko.computed(function () {
                return self.CompleteTotal() - self.Costs();
            }, self);

            self.EditLine = function (line) {
                var dialog = new editLineDialog(line.LineID);
                dialog.OpenModal();
                notifier.closeAll();
            }

            //function CreateButton(name, func, params, notifier, options) {

            //    var _options = { icon: '', buttonStyle: 'btn-primary' };
            //    $.extend(_options, options);

            //    var button = document.createElement("button");
            //    button.innerHTML = '<span class="glyphicon ' + _options.icon + '"></span>' + name;
            //    button.onclick = contextWrapper(func, params, notifier);
            //    button.setAttribute('class', 'btn ' + _options.buttonStyle);
            //    return button;
            //}

            ko.postbox.subscribe("AddValidationError", function (errors) {
                var retry = false;
                $.each(errors, function (index, error) {
                    if (error.CustomState) {
                        var arrayOfStrings = error.CustomState.split('-');
                        var action = arrayOfStrings[0];
                        var id = arrayOfStrings[1];

                        switch (action) {
                            //case 'ServiceDate':
                            //    var checkItem = getCheckItem(id);
                            //    var notification = notifier.Error(error.Value);

                            //    notification.append('<div>' + notifications.serviceDateMessage + '</div>');

                            //    var buttonGroup = $('<div class="row clear_both pull-right btn-toolbar">');
                            //    buttonGroup.append(CreateButton(common.remove, self.checkItemNotPresent, checkItem, notification, { icon: 'glyphicon-remove', buttonStyle: 'btn-danger' }));
                            //    buttonGroup.append(CreateButton(common.adddate, changeServiceDate, checkItem, notification, { icon: 'glyphicon-calendar', buttonStyle: 'btn-default' }));

                            //    notification.append(buttonGroup);

                            //    retry = true;
                            //    break;
                            //case 'ExpiryDate':
                            //    var checkItem = getCheckItem(id);
                            //    var notification = notifier.Error(error.Value);

                            //    notification.append('<div>' + notifications.expiryDateMessage + '</div>');

                            //    var buttonGroup = $('<div class="row clear_both pull-right btn-toolbar">');

                            //    buttonGroup.append(CreateButton(common.remove, self.checkItemNotPresent, checkItem, notification, { icon: 'glyphicon-remove', buttonStyle: 'btn-danger' }));
                            //    buttonGroup.append(CreateButton(common.adddate, changeExpiryDate, checkItem, notification, { icon: 'glyphicon-calendar', buttonStyle: 'btn-default' }));
                            //    notification.append(buttonGroup);

                            //    retry = true;
                            //    break;
                            //case 'VehicleDetail':
                            //    var notification = notifier.Error(error.Value);

                            //    var buttonGroup = $('<div class="row clear_both pull-right">');
                            //    buttonGroup.append(CreateButton(common.update, editVehicle, {}, notification, { icon: 'glyphicon-save', buttonStyle: 'btn-default' }));
                            //    notification.append(buttonGroup);

                            //    retry = true;
                            //    break;
                            //case 'EVHCDetail':
                            //    var notification = notifier.Error(error.Value);

                            //    break;
                            //case 'CustomerDetail':
                            //    var notification = notifier.Error(error.Value);
                            //    break;


                        }
                    } else {
                        notifier.ErrorFlash(error.Value);
                    }
                });

                if (retry) {
                    var notification = notifier.Retry();

                    var buttonGroup = $('<div class="row clear_both pull-right btn-toolbar">');
                    buttonGroup.append(CreateButton(common.submit, self.Process, {}, notification, { icon: 'glyphicon-ok', buttonStyle: 'btn-success' }));

                    if (self._CanCost) {
                        buttonGroup.append(CreateButton(common.cost, self.Cost, {}, notification, { icon: 'glyphicon-share-alt', buttonStyle: 'btn-warning' }));
                    }
                    notification.append(buttonGroup);
                }
            });

            function HandleErrorClick(func, params, notifier) {
                func(params);
                notifier.close();
            }

            var contextWrapper = function (func, params, notifier) {
                var getEventHandler = function () {
                    HandleErrorClick(func, params, notifier);
                };

                return getEventHandler;
            };

            self.Save = function () {
                blocker.Show();
                var data = ajaxHelper.ToServerJson(self);
                ajaxHelper.Post(data, '/Job/Save').done(function (data, textStatus, jqXHR) {
                    window.location.href = '/Job/Quotes' + data;
                }).always(function () {
                });
            };

            self.PrintQuote = function () {
                blocker.Show();
                var data = ajaxHelper.ToServerJson(self);
                ajaxHelper.Post(data, '/Job/Save').done(function (data, textStatus, jqXHR) {
                    window.open('/Reports/Quote/QuoteReport.aspx?id=' + self.Id, 'name', 'height=800,width=1100,scrollbars=1;toolbars=0');
                }).always(function () {
                    blocker.Hide();
                });
                
            };

            self.JobCard = function () {
                blocker.Show();
                var data = ajaxHelper.ToServerJson(self);
                ajaxHelper.Post(data, '/Job/Save').done(function (data, textStatus, jqXHR) {
                    window.open('/Reports/JobCard/JobCardReport.aspx?id=' + self.Id, 'name', 'height=800,width=1100,scrollbars=1;toolbars=0');
                }).always(function () {
                    blocker.Hide();
                });

            };

            self.EmailQuote = function () {
                blocker.Show();
                var data = ajaxHelper.ToServerJson(self);
                ajaxHelper.Post(data, '/Job/Save').done(function (data, textStatus, jqXHR) {
                    ajaxHelper.Post({}, '/Job/_EmailQuote/' + self.Id).done(function (data, textStatus, jqXHR) {
                        notifier.Notify('Quote Sent');
                    }).always(function () {
                    });
                }).always(function () {
                    blocker.Hide();
                });
            };

            self.DeliveryNote = function () {
                blocker.Show();
                var data = ajaxHelper.ToServerJson(self);
                ajaxHelper.Post(data, '/Job/Save').done(function (data, textStatus, jqXHR) {
                    var dialog = new listDeliveryNoteDialog(self.Id);
                    dialog.OpenModal();
                }).always(function () {
                    blocker.Hide();
                });
            };



            self.AddLine = function() {
                var dialog = new lineDialog(self.Id);
                dialog.OpenModal();
            };

            self.DeleteJob = function () {
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
                            ajaxHelper.Delete(JSON.stringify({ 'JobId': self.Id }), '/Job/_Delete')
                                .done(function () {
                                    notifier.Notify('Deleted');
                                    window.location.href = '/Job/Index';
                            }).always(function () {
                                    blocker.Hide();
                                });
                        } else {

                        }

                    }
                });
            }

            self.CopyJob = function () {
                BootstrapDialog.confirm({
                    title: 'Warning',
                    message: 'Are you Sure?',
                    type: BootstrapDialog.TYPE_WARNING,
                    btnCancelLabel: 'Cancel',
                    btnOKLabel: 'Copy',
                    btnOKClass: 'btn-danger',
                    callback: function (result) {
                        if (result) {
                            blocker.Show();
                            ajaxHelper.Post(JSON.stringify({ 'JobId': self.Id }), '/Job/_Copy')
                                .done(function (response) {
                                    window.location.href = '/Job/Edit/' + response.NewJobId;
                                }).always(function () {
                                    blocker.Hide();
                                });
                        } else {

                        }

                    }
                });
            }


            self.DeleteLine = function (line) {
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
                            ajaxHelper.Delete(JSON.stringify({ 'LineId': line.LineID() }), '/Line/_Delete')
                                .done(function () {
                                    notifier.Notify('Deleted');
                                    self._Lines.remove(line);
                                }).always(function () {
                                    blocker.Hide();
                                });
                        } else {

                        }
                    }
                });
            }

            function ChangeStatus(line, status) {
                blocker.Show();
                ajaxHelper.Post(JSON.stringify({ 'LineId': line.LineID(), 'Status': status }), '/Line/_ChangeStatus')
                    .done(function(response) {
                        notifier.Notify('Saved');
                        line._Status(response.Status);
                        self._Status(response.JobStatus);
                    }).always(function() {
                        blocker.Hide();
                    });

            }

            self.Job = function (line) {
                ChangeStatus(line, 4);
            }

            self.Quote = function (line) {
                ChangeStatus(line, 2);
            }

            self.Dead = function (line) {
                ChangeStatus(line, 1);
            }

            self.Complete = function (line) {
                ChangeStatus(line, 8);
            }

            self.AddBOM = function (line) {
                var dialog = new bomDialog(line.LineID());
                dialog.OpenModal();
            }

            self.AddTimesheet = function (line) {
                var dialog = new timesheetDialog(line.LineID());
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

            self.AddContact = function () {
                var dialog = new contactDialog(self.ClientId());
                dialog.OpenModal();
            }

            self.EditContact = function () {
                var dialog = new editContactDialog(self.ContactId());
                dialog.OpenModal();
            }

            self.ListContacts = function () {
                var dialog = new listContactDialog(self.ClientId);
                dialog.OpenModal();
            }


            ko.postbox.subscribe("LineAdded", function (response) {
                var line = new lineModel({
                    LineID: response.LineId,
                    Description: response.Description,
                    JobLineID: response.JobLineId,
                    tblStatusName: response.StatusName,
                    Quantity: response.Quantity,
                    CalculatedUnitPrice: response.CalculatedUnitPrice,
                    ExpectedDeliveryDateString: response.ExpectedDeliveryDateString,
                    DeliveryComments: response.DeliveryComments,
                    DrawingNumber: response.DrawingNumber,
                    tblFileFileName: response.FileName,
                    tblFileContentType: response.ContentType,
                    FilePath: response.FilePath
                });

                self._Lines.push(line);
                self.EditLine(line);
            });

            ko.postbox.subscribe("LineEdited", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    line.Description(response.Description);
                    line.Quantity(response.Quantity);
                    line.UnitPrice(response.CalculatedUnitPrice);
                    line.ExpectedDeliveryDate(response.ExpectedDeliveryDateString);
                    line.DeliveryComments(response.DeliveryComments);
                    line.DrawingNumber(response.DrawingNumber);
                    line.FileName(response.FileName);
                    line.ContentType(response.ContentType);
                    line.FilePath(response.FilePath);
                }
            });


            ko.postbox.subscribe("BOMAdded", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    line._BillOfMaterials.push(new bomModel({
                        PurchaseOrderID: response.PurchaseOrderID,
                        UniqueId: response.UniqueId,
                        Description: response.Description,
                        Cost: response.Cost,
                        Quantity: response.Quantity,
                        Comments: response.Comments,
                        PurchaseOrderDate: response.PurchaseOrderDateString,
                        SupplierRef: response.SupplierRef
                    }));
                    line.UnitPrice(response.CalculatedUnitPrice);
                }
            });

            ko.postbox.subscribe("BOMEdited", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    var billOfMaterials = line.getBillOfMaterials(response.BillOfMaterialsId);
                    if (billOfMaterials) {
                        billOfMaterials.Description(response.Description);
                        billOfMaterials.Cost(response.Cost);
                        billOfMaterials.Quantity(response.Quantity);
                        billOfMaterials.Comments(response.Comments);
                        billOfMaterials.PurchaseOrderDate(response.PurchaseOrderDateString);
                        billOfMaterials.SupplierRef(response.SupplierRef);
                    }
                    line.UnitPrice(response.CalculatedUnitPrice);
                }
            });

            ko.postbox.subscribe("TimesheetAdded", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    line._Timesheets.push(new timesheetModel({
                        TimesheetID: response.TimesheetID,
                        UniqueId: response.UniqueId,
                        Comments: response.Comments,
                        Hours: response.Hours,
                        HourlyRate: response.HourlyRate,
                        TimesheetDateString: response.TimesheetDateString,
                        OperativeName: response.OperativeName
                    }));
                }
            });

            ko.postbox.subscribe("TimesheetEdited", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    var timesheet = line.getTimesheet(response.TimesheetId);
                    if (timesheet) {
                        timesheet.Comments(response.Comments);
                        timesheet.Hours(response.Hours);
                        timesheet.HourlyRate(response.HourlyRate);
                        timesheet.TimesheetDate(response.TimesheetDateString);
                    }
                }
            });

            ko.postbox.subscribe("BOMDeleted", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    line._BillOfMaterials.remove(function (item) { return item.BillOfMaterialsID() == response.BillOfMaterialsId; });
                    line.UnitPrice(response.CalculatedUnitPrice);
                }
            });

            ko.postbox.subscribe("TimesheetDeleted", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    line._Timesheets.remove(function(item) { return item.TimesheetID() == response.TimesheetId; });
                }
            });

            ko.postbox.subscribe("DrawingDeleted", function (response) {
                var line = self.getLine(response.LineId);
                if (line) {
                    line.FileName('');
                    line.ContentType('');
                    line.FilePath('');
                }
            });


            ko.postbox.subscribe("ContactAdded", function (response) {
                self.ContactId(response.ContactId);
                self.Contact(response.Forename + " " + response.Surname);
            });

            ko.postbox.subscribe("ContactEdited", function (response) {
                self.Contact(response.Forename + " " + response.Surname);
            });

            ko.postbox.subscribe("LineStatusChanged", function (response) {
                var line = self.getLine(response.LineId);
                self._Status(response.JobStatus);
                if (line) {
                    line._Status(response.Status);
                }
            });

            ko.postbox.subscribe("DeliveryNoteCreated", function (response) {
                alert(response.DeliveryNoteURL);
                window.open(response.DeliveryNoteURL, response.DeliveryNoteURL, 'height=800,width=1100,scrollbars=1;toolbars=0');
            });

            self.getLine = function (lineId) {
                for (var i = 0; i < self._Lines().length; i++) {
                    if (self._Lines()[i].LineID() == lineId) {
                        return self._Lines()[i];
                    }
                }
            };

        }
    });