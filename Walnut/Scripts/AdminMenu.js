$(function () {
    $('[data-admin-menu-de-walnut]').hover(function () { // interceptamos el evento "hover" (osea, click sobre el menu)
        // pasandole una funcion, que lo que hace es...
        $('[data-admin-menu-de-walnut]').toggleClass('open'); //"quitar" la parte del menu que contiene la lista de opciones,
        // sustituyendola por otra clase vacia, a la que hemos llamado "open", aunque podriamos haberla llamado de
        // cualquier otra forma (el nombre es irelevante).
    });
});






