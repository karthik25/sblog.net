IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'DisqusEnabled')
BEGIN
	INSERT INTO sBlog_Settings VALUES('DisqusEnabled', NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogDisqusShortName')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogDisqusShortName', NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogDbVersion')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogDbVersion', NULL);
END

UPDATE sBlog_Settings SET KeyValue = '02_01' WHERE KeyName = 'BlogDbVersion';
