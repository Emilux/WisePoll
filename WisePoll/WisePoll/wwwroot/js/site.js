// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function()
{
    let inputElm = document.querySelector('.taginput'), 
        tagify = new Tagify (inputElm, {
            delimiters: ",| ",
            duplicates: true,
            originalInputValueFormat: valuesArr => valuesArr.map(item => item.value).join(','),
            //pattern: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
        });
});