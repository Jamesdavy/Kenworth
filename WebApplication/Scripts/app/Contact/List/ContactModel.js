define(['knockout'], function (ko) {
    return function ContactModel(model) {
        var self = this;

        self.ContactId = ko.observable(model.ContactID);
        self.Forename = ko.observable(model.Forename);
        self.Surname = ko.observable(model.Surname);
        self.Position = ko.observable(model.Position);
        self.Phone = ko.observable(model.Phone);
        self.Email = ko.observable(model.Email);
        self.Status = ko.observable(model.Status);
    }
});