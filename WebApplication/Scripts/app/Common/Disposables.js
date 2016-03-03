define([], function () {
    return DisposableHelper = (function () {
        var self = this;

        var disposables = [];

        return {
            addDisposable: function (key, value) {
                disposables[key] = value;
            },
            //getDisposable: function (key) {
            //    return disposables[key];
            //},
            dispose: function (key) {
                var disposable = disposables[key];
                if (disposable != undefined)
                    disposable.dispose();
            }
        }
    })();
});