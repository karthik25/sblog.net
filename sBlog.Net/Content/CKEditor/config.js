/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    config.height = 500;
    config.enterMode = CKEDITOR.ENTER_BR;

    config.toolbar = 'CustomsBlogToolBar';

    config.toolbar_CustomsBlogToolBar =
    [
	    { name: 'document', items: ['Source', '-', 'Templates'] },
	    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', '-', 'Undo', 'Redo'] },
	    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline'] },
	    { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Blockquote', 'CreateDiv',
	    '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock']
	    },
	    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	    { name: 'insert', items: ['Image'] },
	    { name: 'colors', items: ['TextColor', 'BGColor'] },
	    { name: 'tools', items: ['Maximize', '-', 'About'] }
    ];

    config.filebrowserBrowseUrl = siteRoot + "Admin/Uploads/SelectFile";
    config.filebrowserWindowWidth = 500;
    config.filebrowserWindowHeight = 650;
    config.filebrowserUploadUrl = siteRoot + "Admin/Uploads/UploadFile";
};
