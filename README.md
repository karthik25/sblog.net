# sBlog.Net

In a very few words, sBlog.Net is a minimalistic blog engine. This project was heavily inspired by wordpress, though there are a lot more to 
come in the following days! You will be able to do all of the normal activities that you could do in a blog like adding a post or page, 
adding categories or tags, adding additional authors and many more. If I have to describe sBlog.Net in a single sentence, I would say,

"For the love of asp.net mvc and wordpress!"

To get started, refer to this short article! You just need an instance of Visual Studio 2010 with ASP.Net MVC 3, MS SQL server (express 
should also be fine) and optionally IIS.

Thanks for trying out sBlog.Net!<br/>
http://sblogproject.net

## Notes for sBlog.Net v2.0

```text
Notice: This branch is intended to be a play-ground for globalization/localization. So you might notice some 
unexpected results. If you want a more stable branch, try the "master" branch
```

Follow these steps to start experimenting with v2.0. This repository is for the development version of sBlog.Net.
For the released version go to https://github.com/karthik25/sblog.net/tree/sblog.net-v1.0-release.

To use this dev version, get the latest source from github and extract the source. Then run the sBlog.Net.DB\v2.0\Update\sblog_02_01.sql
and sBlog.Net.DB\v2.0\Update\sblog_02_02.sql files by selecting your sBlog.Net database. Now, you are all set to use the new version of 
sBlog.Net! Following are the major enhancements in this development version:

* Role based access - By default "1" user belongs to the "Super admin" role. All other users will be under the "Author" role.
* Author listing page and posts by author
* Disqus support - Go to the settings page to play with it

In the near future, there will be a windows based application to manage the database!
