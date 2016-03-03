define(['knockout', 'app/Common/Notification', 'app/Common/UIBlocker'], function (ko, notification, uiBlocker) {
    return AjaxHelper = (function () {
        var self = this;
        var notifier = new notification();
        var blocker = new uiBlocker();

        return {
            Post: function (data, route) {
                return $.ajax({
                    url: route,
                    contentType: 'application/json',
                    type: "POST",
                    data: data,
                    dataType: 'json'
                }).fail(function (jqXHr, textStatus, errorThrown) {
                    error(jqXHr, textStatus, errorThrown);
                });

                function error(jqXHr, textStatus, errorThrown) {
                    blocker.Hide();
                    notifier.closeAll();
                    if (jqXHr.status == 403) {
                        window.location.href = '@FormsAuthentication.LoginUrl';
                    } else if (jqXHr.status == 412) {
                        var response = JSON.parse(jqXHr.responseText);
                        if (confirm(response.Warning) == true) {
                            window.location.reload();
                        }
                    } else if (jqXHr.status == 500) {
                        alert(jqXHr.responseText);
                    } else if (jqXHr.status == 0) {
                        alert("Undefined Error, Please try again");
                    } else {
                        ////alert(jqXHr.responseText);
                        //var validationResult = new KoServerValidationErrors().validateModel(viewModel, JSON.parse(jqXHr.responseText));
                        //for (key in validationResult.errors()) {
                        //    var error = new ServerError(key, validationResult.errors()[key]);
                        //    viewModel.Errors.push(error);
                        //}
                        //viewModel.errors.showAllMessages(true);
                        alert(jqXHr.responseText);
                        var serverErrors = JSON.parse(jqXHr.responseText);
                        var errors = [];
                        $.each(serverErrors.ModelState, function (index, element) {
                            var error = new ServerError(element.Key, element.Value, element.CustomState);
                            errors[errors.length] = error;

                        });

                        ko.postbox.publish("AddValidationError", errors);
                    }
                }

                function ServerError(key, value, customState) {
                    var self = this;
                    self.Key = key;
                    self.Value = value;
                    self.CustomState = customState;
                }
            },
            GetPartialView: function (data, route) {
                return $.ajax({
                    url: route,
                    contentType: 'application/json',
                    cache: false,
                    type: "get",
                    dataType: 'html',
                    data: data
                }).fail(function (jqXHr, textStatus, errorThrown) {
                    error(jqXHr, textStatus, errorThrown);
                });

                function error(jqXHr, textStatus, errorThrown) {
                    if (jqXHr.status == 403) {
                        window.location.href = routes.GetRoute('Login');
                    } else {
                        alert("fail" + jqXHr.responseText);
                    }
                }
            },
            Delete: function (data, route) {
                return $.ajax({
                    url: route,
                    contentType: 'application/json',
                    type: "delete",
                    data: data,
                    dataType: 'json'
                }).fail(function (jqXHr, textStatus, errorThrown) {
                    error(jqXHr, textStatus, errorThrown);
                });

                function error(jqXHr, textStatus, errorThrown) {
                    if (jqXHr.status == 403) {
                        window.location.href = routes.GetRoute('Login');
                    } else {
                        alert("fail:" + jqXHr.responseText + jqXHr.status);
                    }
                }
            },
            ToServerJson : function(model) {
                return ko.toJSON(model, function(key, value) {
                    if (key.toString().indexOf("_") === 0) {
                        return;
                    } else {
                        return value;
                    }
                });
            }
        }
    })();
});
