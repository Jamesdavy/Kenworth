requirejs.config({
    //urlArgs: "bust=" + new Date().getTime(),
    urlArgs: "v=0.6", 
    baseUrl: '/Scripts',
    waitSeconds: 40,
    paths: {
        app: 'app',
        jquery: 'jquery-1.10.2',
        'jquery.autocomplete.min': 'jquery.autocomplete.min',
        'jquery-migrate-1.2.1.min': 'jquery-migrate-1.2.1.min',
        'knockout': 'knockout-3.4.0',
        'knockout.validation': 'knockout.validation.min',
        'knockout-postbox': 'knockout-postbox.min',
        'kendo': 'kendo/kendo.web.min',
        'knockout-kendo': 'knockout-kendo.min',
        'jquery-ui': 'jquery-ui-1.11.4.min',
        'bootstrap': 'bootstrap',
        'bootstrap-dialog': 'bootstrap-dialog'
    },
    shim: {
        'jquery.autocomplete.min': {
            deps: ['jquery', 'jquery-migrate-1.2.1.min']
        },
        'jquery-migrate-1.2.1.min': {
            deps: ['jquery']
        },
        'kendo' : {
            deps: ['jquery']
        },
        'knockout-kendo': {
            deps: ["jquery", "kendo", "knockout"]
        },
        'jquery-ui': {
            deps: ['jquery']
        },
        'bootstrap-dialog': {
            deps: ['jquery', 'bootstrap']
        },
        'bootstrap': {
            deps: ['jquery']
        }
    }
});
