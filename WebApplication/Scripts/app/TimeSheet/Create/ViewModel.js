define(['knockout',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'app/Common/UIBlocker',
    'app/Common/Notification',
    'knockout.validation',
    'knockout-postbox',
    'jquery.autocomplete.min'], function (ko, ajaxHelper, disposables, uiBlocker, notification) {
    return function ViewModel(model) {
        var self = this;

        var blocker = new uiBlocker();
        var notifier = new notification();
        var initialDate = model.TimesheetDateString;

        self.LineId = ko.observable(model.LineID);
        self.Comments = ko.observable();
        self.JobLineDescription = ko.observable().extend({ required: true });
        self.UserId = ko.observable().extend({ required: true });
        self.UserName = ko.observable().extend({ required: true });
        self.Hours = ko.observable().extend({ required: true });
        self.HourlyRate = ko.observable(model.HourlyRate).extend({ required: true });
        self.TimesheetDate = ko.observable(model.TimesheetDateString).extend({ required: true });

        ko.validation.init({
            registerExtenders: true,
            messagesOnModified: false,
            insertMessages: false,
            parseInputAttributes: true,
            messageTemplate: null,
            decorateInputElement: true,
            errorElementClass: 'has-error',
            errorAsTitle: true,
            grouping: {
                deep: true
            }
        }, true);

        var addTimesheetSubscription = ko.postbox.subscribe("AddTimesheet", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/Timesheet/_Create').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                self.Comments('');
                self.Hours();
                self.HourlyRate();
                self.TimesheetDate(initialDate);
                if (callback) {
                    
                    callback(response);
                }
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("AddTimesheet", addTimesheetSubscription);

        $("#LineSearch").autocompleteold('/Json/_GetMatchingJobLineID', {
            autoFill: false,
            mustMatch: false,
            matchContains: false,
            cacheLength: 12,
            formatItem: function (data, index, max) {
                return data[1];
            },
            formatMatch: function (data, index, max) {
                return data[1];
            },
            formatResult: function (data, index, max) {
                return data[1];
            }
        }).result(function (event, data, formatted) {
            if (data) {
                self.JobLineDescription(data[1]);
                self.LineId(data[0]);
            } else {
                self.JobLineDescription('');
                self.LineId(0);
            }
        });

        $("#OperativeSearch").autocompleteold('/Json/_GetMatchingOperatives', {
            autoFill: false,
            mustMatch: false,
            matchContains: false,
            cacheLength: 12,
            formatItem: function (data, index, max) {
                return data[1];
            },
            formatMatch: function (data, index, max) {
                return data[1];
            },
            formatResult: function (data, index, max) {
                return data[1];
            }
        }).result(function (event, data, formatted) {
            if (data) {
                self.UserName(data[1]);
                self.UserId(data[0]);
            } else {
                self.UserName('');
                self.UserId(0);
            }
        });

    }
});