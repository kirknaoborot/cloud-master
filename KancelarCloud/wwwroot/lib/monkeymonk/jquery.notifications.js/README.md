jQuery.notifications Plugin - v 1.0
==================

Notifications System inspired by Growl.

Project site: http://monkeymonk.github.com/jquery.notifications.js/

Demo: http://monkeymonk.be/jquery.notifications.js/demo/


## Usage

### Basic

First of all, include `jquery.notifications.js` and `jquery.notifications.css` in your HTML then start to use jQuery.notifications.

``` javascript
$(document).ready(function() {
    $.notifications({
        text: 'Lorem ipsum',
        content: '<b>Lorem ipdum</b> dolor sit amet...',
        image: 'http://lorempixel.com/40/40/abstract/'
    });
});
```

### Options

``` javascript
{
    className: 'notification',

    alive: 4000,
    fadeIn: 600,
    fadeOut: 800,
    sticky: false,

    // Template
    tpl: '<div class="{className}"><div class="{className}-img"><img src="{image}" alt="" /></div><div class="{className}-content"><div class="{className}-title">{title}</div>{content}</div></div>',

    // Callback
    onShow: function () {},
    onHide: function () {}
}
```

### Methods

#### jQuery.notifications('add', options);

jQuery.notifications use a tiny template system that let you an easy way to customize your notifications.

``` javascript
$.notifications('add', {
    title: 'Lorem ipsum',
    content: '<b>Lorem ipsum</b> dolor sit amet...',

    className: 'simple',

    tpl: '<div class="{className}"><h1>{title}</h1><p>{content}</p></div>'
});

// same as $.notifications(params);

```

#### jQuery.notifications('remove', onHide);

By default, that remove the last notification that was added.
You can pass an identifier to targetting a specific one.
You can also pass a callback, if not the `onHide` will be used (if is set).

``` javascript
$.notifications('remove', onHide);

// or

$.notifications('remove', 3, onHide);

```

#### jQuery.notifications('removeAll', onHide);

Remove all notifications.
You can pass a callback, if not the `onHide` will be used (if is set).

``` javascript
$.notifications('removeAll', callback);

```

### Data-API
Every options are accessible via [data-*].

``` html
<button type="button" data-toggle="notification" data-title="It's a notification" data-content="With <b>awesome content</b>">Toggle notification</button>
```


## Browsers: Tested and Working In

- IE 6, 7, 8, 9, 10
- Firefox 3+
- Opera 10+
- Safari 4+

