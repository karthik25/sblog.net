PRINT 'Running sblog_02_01'

INSERT INTO sBlog_Settings VALUES('DisqusEnabled', NULL);
INSERT INTO sBlog_Settings VALUES('BlogDisqusShortName', NULL);
INSERT INTO sBlog_Settings VALUES('BlogDbVersion', NULL);

UPDATE sBlog_Settings SET KeyValue = '02_01' WHERE KeyName = 'BlogDbVersion';
