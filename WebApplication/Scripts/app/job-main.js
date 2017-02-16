define(function (require) {
    var $ = require('jquery'),
        ko = require('knockout'),
        vm = require('app/Job/Create/ViewModel');
        require('jquery-migrate-1.2.1.min');
        require('jquery.autocomplete.min');
        require('knockout.validation');
    return function () {
        var self = this;
        var ViewModel = "";

        self.setDate = function(date) {
            ViewModel.ExpectedDeliveryDate(date);
        }

        self.Initialise = function (model) {
            ViewModel = new vm(model);
            
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

            ViewModel.errors = ko.validation.group(ViewModel, { deep: true });
            ko.applyBindingsWithValidation(ViewModel);
            ViewModel.errors.showAllMessages();
            //ko.applyBindings(ViewModel, document.getElementById("JobDetails"));

            $("#Client").autocompleteold('/Json/_GetMatchingClients', {
                autoFill: false,
                mustMatch: true,
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
                    ViewModel.ClientId(data[0]);
                    ViewModel.Client(data[1]);
                } else {
                    ViewModel.ClientId(0);
                    ViewModel.Client('');
                }
            });

            $("#Contact").autocompleteold('/Json/_GetMatchingContacts', {
                autoFill: false,
                mustMatch: false,
                matchContains: false,
                cacheLength: 12,
                extraParams: { 'id' : function () { return ViewModel.ClientId(); } },
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
                    ViewModel.ContactId(data[0]);
                    ViewModel.Contact(data[1]);
                } else {
                    ViewModel.ContactId('');
                    ViewModel.Contact('');
                }
            });
        }

        
    };
});