define(['knockout'], function (ko) {
    return function TimesheetModel(model) {
        var self = this;

        self.TimesheetID = ko.observable(model.TimesheetID);
        self.UniqueId = ko.observable(model.UniqueId);
        self.Comments = ko.observable(model.Comments);
        self.Hours = ko.observable(model.Hours);
        self.HourlyRate = ko.observable(model.HourlyRate);
        self.TimesheetDate = ko.observable(model.TimesheetDateString);
        self.OperativeName = ko.observable(model.OperativeName);

        self.Total = ko.computed(function () {
            return self.Hours() * self.HourlyRate();
        }, self);

    }
});