-- =========================================================
-- Library Management System — Schema (v2: reviewed & fixed)
-- =========================================================

-- 1. إنشاء قاعدة البيانات
CREATE DATABASE LibraryDB;
GO
USE LibraryDB;
GO

-- 2. جدول الأشخاص (الأساس المشترك لأي مستخدم أو مشترك)
CREATE TABLE People (
    PersonID   INT IDENTITY(1,1) PRIMARY KEY,
    FirstName  NVARCHAR(50) NOT NULL,
    LastName   NVARCHAR(50) NOT NULL,
    Phone      NVARCHAR(20),
    Email      NVARCHAR(100)
);
GO

-- 3. جدول المستخدمين (الموظفين اللي بيدخلوا عالنظام)
CREATE TABLE Users (
    UserID     INT IDENTITY(1,1) PRIMARY KEY,
    PersonID   INT NOT NULL FOREIGN KEY REFERENCES People(PersonID),
    Username   NVARCHAR(50) NOT NULL UNIQUE,
    Password   NVARCHAR(100) NOT NULL,
    IsActive   BIT DEFAULT 1
);
GO

-- 4. جدول المشتركين (أعضاء المكتبة اللي بيستعيروا كتب)
CREATE TABLE Members (
    MemberID          INT IDENTITY(1,1) PRIMARY KEY,
    PersonID          INT NOT NULL FOREIGN KEY REFERENCES People(PersonID),
    SubscriptionDate  DATE DEFAULT GETDATE()
);
GO

-- 5. جدول المؤلفين (جديد — بدل ما يكون اسم المؤلف نص حر بجدول الكتب)
CREATE TABLE Authors (
    AuthorID   INT IDENTITY(1,1) PRIMARY KEY,
    FirstName  NVARCHAR(50) NOT NULL,
    LastName   NVARCHAR(50) NOT NULL
);
GO

-- 6. جدول الكتب
CREATE TABLE Books (
    BookID     INT IDENTITY(1,1) PRIMARY KEY,
    Title      NVARCHAR(200) NOT NULL,
    ISBN       VARCHAR(20) NULL,          -- مش إجباري: كتب قديمة/محلية ممكن ما يكون إلها ISBN
    AuthorID   INT NOT NULL FOREIGN KEY REFERENCES Authors(AuthorID),
    Quantity   INT NOT NULL DEFAULT 1
    -- ملاحظة: ما في عمود IsAvailable — الكمية المتاحة فعلياً رح نحسبها بـ query لاحقاً
);
GO

-- Filtered unique index: يمنع تكرار ISBN، بس بيسمح بكذا كتاب بدون ISBN (NULL)
CREATE UNIQUE INDEX UQ_Books_ISBN
    ON Books(ISBN)
    WHERE ISBN IS NOT NULL;
GO

-- 7. جدول الاستعارات (يربط المشترك بالكتاب)
CREATE TABLE Borrowings (
    BorrowingID  INT IDENTITY(1,1) PRIMARY KEY,
    MemberID     INT NOT NULL FOREIGN KEY REFERENCES Members(MemberID),
    BookID       INT NOT NULL FOREIGN KEY REFERENCES Books(BookID),
    BorrowDate   DATE DEFAULT GETDATE(),
    ReturnDate   DATE NULL
    -- ملاحظة: ما في عمود Status — ReturnDate = NULL يعني لسا مستعار، وإذا فيها تاريخ يعني تم الإرجاع
);
GO

CREATE TABLE Fines (
    FineID       INT IDENTITY(1,1) PRIMARY KEY,
    BorrowingID  INT NOT NULL FOREIGN KEY REFERENCES Borrowings(BorrowingID),
    FineAmount   SMALLMONEY NOT NULL CHECK (FineAmount > 0),
    FineDate     DATE DEFAULT GETDATE(),
    PaidDate     DATE NULL
);
GO

-- تأكيد إنشاء الجداول
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';
