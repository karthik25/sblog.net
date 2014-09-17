IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'EditorType')
BEGIN
	INSERT INTO sBlog_Settings VALUES('EditorType', 'html');
END
