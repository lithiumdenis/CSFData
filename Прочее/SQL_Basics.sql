SELECT  SURNAME, NAME, KURS
FROM STUDENT
WHERE STIPEND > 99 AND CITY = '�������';

SELECT *
FROM STUDENT
WHERE NAME LIKE '�%' OR NAME LIKE '�%';

SELECT *
FROM EXAM_MARKS
WHERE MARK IS NOT NULL;

SELECT UPPER(STUDENT_ID) || '; �������: ' || UPPER(SURNAME) || '; ���: ' || UPPER(NAME) || '; ������ ���������: ' || 
UPPER(STIPEND) || '; ���� ��������: ' || UPPER(BIRTHDAY)
FROM STUDENT;

SELECT  '���: ' || UNIV_ID || ';' || UNIV_NAME || '-�.' || UPPER(CITY) || '; �������: ' || ROUND(RATING, -2) * 2
FROM UNIVERSITY;

SELECT COUNT(DISTINCT SUBJ_ID)
FROM EXAM_MARKS;

SELECT STUDENT_ID, MIN(MARK)
FROM EXAM_MARKS
GROUP BY STUDENT_ID;

SELECT MIN(SURNAME) 
FROM STUDENT
WHERE NAME LIKE '�%';

SELECT SUBJ_NAME, MAX(SEMESTER)
FROM SUBJECT
GROUP BY SUBJ_NAME;

SELECT EXAM_DATE, COUNT(STUDENT_ID)
FROM EXAM_MARKS
GROUP BY EXAM_DATE;

SELECT  STUDENT_ID, AVG(MARK)
FROM EXAM_MARKS
GROUP BY STUDENT_ID;

SELECT STUDENT_ID, SURNAME, STIPEND * 1.2
FROM STUDENT
WHERE STIPEND IS NOT NULL
ORDER BY 2 DESC;

SELECT SEMESTER, SUBJ_NAME, SUBJ_ID
FROM SUBJECT
ORDER BY SEMESTER DESC;

SELECT EXAM_DATE, SUM(MARK)
FROM EXAM_MARKS
GROUP BY EXAM_DATE
ORDER BY 2 DESC;

SELECT DISTINCT *
FROM EXAM_MARKS 
WHERE STUDENT_ID IN (SELECT DISTINCT STUDENT_ID FROM STUDENT WHERE SURNAME = '������');

SELECT ST.NAME, ST.SURNAME
FROM STUDENT ST, EXAM_MARKS EM
WHERE EM.MARK > 
(SELECT AVG(MARK)
FROM EXAM_MARKS 
) AND EM.STUDENT_ID = ST.STUDENT_ID AND EM.SUBJ_ID = 2;

SELECT NAME, STUDENT_ID
FROM  STUDENT ST1
WHERE ST1.STIPEND = 
(SELECT MAX(ST2.STIPEND)
FROM STUDENT ST2
WHERE ST1.CITY = ST2.CITY
);

SELECT NAME, STUDENT_ID
FROM  STUDENT ST1
WHERE ST1.CITY NOT IN
(
SELECT UN.CITY
FROM UNIVERSITY UN
);

SELECT *
FROM STUDENT ST
WHERE EXISTS
(SELECT *
FROM UNIVERSITY UN
WHERE RATING > 300 AND UN.UNIV_ID = ST.UNIV_ID
);

SELECT *
FROM STUDENT ST, UNIVERSITY UN
WHERE RATING > 300 AND UN.UNIV_ID = ST.UNIV_ID;

SELECT *
FROM STUDENT ST
WHERE EXISTS
(
SELECT *
FROM UNIVERSITY UN
WHERE ST.CITY = UN.CITY AND ST.UNIV_ID <> UN.UNIV_ID
);

SELECT SU.SUBJ_NAME
FROM SUBJECT SU
WHERE 1000 < 
(SELECT COUNT(EM.MARK)
FROM EXAM_MARKS EM
WHERE SU.SUBJ_ID = EM.SUBJ_ID AND EM.MARK > 2
);

SELECT UN.UNIV_NAME
FROM UNIVERSITY UN
WHERE UN.RATING >
(SELECT UN2.RATING
FROM UNIVERSITY UN2
WHERE UN2.UNIV_NAME = '����������� ��������������� �����������'
);

SELECT *
FROM STUDENT
WHERE CITY <> ALL
(
SELECT CITY
FROM UNIVERSITY
);

SELECT UNIV_NAME, CITY, '�������'
FROM UNIVERSITY
WHERE RATING > 300
UNION
SELECT UNIV_NAME, CITY, '������'
FROM UNIVERSITY
WHERE RATING < 300;

SELECT SURNAME, '��������'
FROM STUDENT ST
WHERE NOT EXISTS
(SELECT *
FROM EXAM_MARKS EM
WHERE EM.STUDENT_ID = ST.STUDENT_ID AND EM.MARK < 3 AND EM.MARK IS NOT NULL
)
UNION
SELECT SURNAME, '�� ��������'
FROM STUDENT ST
WHERE EXISTS
(SELECT *
FROM EXAM_MARKS EM
WHERE EM.STUDENT_ID = ST.STUDENT_ID AND EM.MARK < 3 AND EM.MARK IS NOT NULL
)
UNION
SELECT SURNAME, '�� ������'
FROM STUDENT ST
WHERE EXISTS
(SELECT *
FROM EXAM_MARKS EM
WHERE EM.STUDENT_ID = ST.STUDENT_ID AND EM.MARK IS NULL
);

SELECT SURNAME, '�������'
FROM STUDENT
WHERE UNIV_ID = 
(SELECT UNIV_ID
FROM UNIVERSITY
WHERE UNIV_NAME = '����������� ��������������� �����������'
)
UNION
SELECT SURNAME, '�������������'
FROM LECTURER
WHERE UNIV_ID = 
(SELECT UNIV_ID
FROM UNIVERSITY
WHERE UNIV_NAME = '����������� ��������������� �����������'
);

SELECT SURNAME, SUBJ_ID
FROM STUDENT JOIN EXAM_MARKS //������ ��� NULL
ON STUDENT.STUDENT_ID = EXAM_MARKS.STUDENT_ID;

SELECT SURNAME, SUBJ_ID
FROM STUDENT LEFT OUTER JOIN EXAM_MARKS  //���
ON STUDENT.STUDENT_ID = EXAM_MARKS.STUDENT_ID;

SELECT SURNAME, SUBJ_NAME, MARK
FROM STUDENT ST, SUBJECT SU, EXAM_MARKS EM
WHERE SU.SUBJ_ID = EM.SUBJ_ID AND ST.STUDENT_ID = EM.STUDENT_ID AND MARK >= 4;

SELECT UN.UNIV_NAME, MAX(ST.STIPEND)
FROM UNIVERSITY UN, STUDENT ST
WHERE UN.UNIV_ID = ST.UNIV_ID AND UN.RATING > 300
GROUP BY UN.UNIV_NAME;

SELECT SURNAME, RATING
FROM UNIVERSITY LEFT OUTER JOIN STUDENT
ON UNIVERSITY.UNIV_ID = STUDENT.UNIV_ID
ORDER BY 1;

INSERT INTO SUBJECT (SEMESTER, SUBJ_NAME, HOUR, SUBJ_ID)
VALUES (4, '�����������', 72, 201);

INSERT INTO STUDENT (NAME, SURNAME, KURS, UNIV_ID, CITY, BIRTHDAY, STIPEND, STUDENT_ID)
VALUES ('�����', '�������', 1, 12, '�������', NULL, NULL, 45460);

DELETE FROM EXAM_MARKS
WHERE STUDENT_ID = 100;

UPDATE UNIVERSITY
SET RATING = RATING + 5
WHERE CITY = '�������';

CREATE TABLE GIRLFRIENDS
(NAME         VARCHAR(60) NOT NULL,
 SURNAME      VARCHAR(60),
 BIRTHDAY     DATE,
 FIRSTDATE    DATE,
 AGE          INTEGER,
 HAIR_COLOR   VARCHAR(60),
 PASSPORT     INTEGER PRIMARY KEY
);

DROP TABLE GIRLFRIENDS;