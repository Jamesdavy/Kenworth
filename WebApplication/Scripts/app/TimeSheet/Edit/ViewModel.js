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

        self.TimesheetId = ko.observable(model.TimesheetID);
        self.Comments = ko.observable(model.Comments);
        //self.JobLineDescription = ko.observable().extend({ required: true });
        //self.UserId = ko.observable().extend({ required: true });
        //self.UserName = ko.observable().extend({ required: true });
        self.Hours = ko.observable(model.Hours).extend({ required: true });
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

        var addTimesheetSubscription = ko.postbox.subscribe("EditTimesheet", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            alert(data);
            new ajaxHelper.Post(data, '/Timesheet/_Edit').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                if (callback) {
                    callback(response);
                }
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("EditTimesheet", addTimesheetSubscription);

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