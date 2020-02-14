-- Task 1.1 -------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE ADD_CUST_TO_DB(pcustid NUMBER, pcustname VARCHAR2) AS
SALES_YTD NUMBER := 0;
STATUS VARCHAR(2) := 'OK';
out_of_range EXCEPTION;
BEGIN
    IF pcustid BETWEEN 1 AND 499 THEN
        INSERT INTO Customer VALUES (pcustid,pcustname,sales_ytd,status);
        DBMS_OUTPUT.PUT_LINE('Added '||status);
    ELSE    
            RAISE out_of_range;
    END IF;
EXCEPTION
WHEN DUP_VAL_ON_INDEX THEN
raise_application_error(-20012,'. Duplicate customer ID');
WHEN out_of_range THEN
raise_application_error(-20024,'. Customer ID out of range');
WHEN OTHERS THEN
raise_application_error(-20000,'. Use value of sqlerrm');
END;
/
CREATE OR REPLACE PROCEDURE ADD_CUSTOMER_VIASQLDEV(pcustid NUMBER, pcustname VARCHAR2) AS

BEGIN
DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
DBMS_OUTPUT.PUT_LINE('Adding Customer.ID: '||pcustid||' Name: '||pcustname);
ADD_CUST_TO_DB(pcustid,pcustname);
COMMIT;
EXCEPTION
WHEN OTHERS THEN
DBMS_OUTPUT.PUT_LINE(sqlerrm);
END;
/


-- Task 1.2 -------------------------------------------------------------------

CREATE OR REPLACE function DELETE_ALL_CUSTOMERS_FROM_DB return number AS
vCount number;
BEGIN
DELETE FROM Customer;
vCount:= sql%rowcount;
return vCount;
EXCEPTION
WHEN OTHERS THEN
raise_application_error(-20000,SQLERRM);
END;
/
CREATE OR REPLACE PROCEDURE DELETE_ALL_CUSTOMERS_VIASQLDEV AS
--errmsg VARCHAR2(50);
rcount NUMBER;
BEGIN
DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
DBMS_OUTPUT.put_line('Deleting all Customer rows');

dbms_output.put_line(DELETE_ALL_CUSTOMERS_FROM_DB || ' ROWS DELETED:');
COMMIT;
EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(SQLERRM);
END;
/



-- Task 1.3 -------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE ADD_PRODUCT_TO_DB(pprodid NUMBER,pprodname VARCHAR2,pprice NUMBER) AS
SALES_YTD NUMBER := 0;
prod_out_range EXCEPTION;
price_out_range EXCEPTION;
BEGIN
IF pprodid BETWEEN 1000 AND 2500 THEN
IF pprice BETWEEN 0 AND 999.99 THEN
INSERT INTO Product VALUES(pprodid,pprodname,pprice,SALES_YTD);
ELSE
RAISE price_out_range;
END IF;
ELSE
RAISE prod_out_range;
END IF;
EXCEPTION
WHEN DUP_VAL_ON_INDEX THEN
raise_application_error(-20032,'Duplicate customer ID');
WHEN prod_out_range THEN
raise_application_error(-20044, 'Product ID out of range');
WHEN price_out_range THEN
raise_application_error(-20056, 'Price out of range');
WHEN OTHERS THEN
raise_application_error(-20000,sqlerrm);
END;
/

CREATE OR REPLACE PROCEDURE ADD_PRODUCT_VIASQLDEV(pprodid NUMBER,pprodname VARCHAR2, pprice NUMBER) AS
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Adding Product. ID: '||pprodid||' Name '||pprodname);
ADD_PRODUCT_TO_DB(pprodid,pprodname,pprice);
dbms_output.put_line('Product Added OK');
COMMIT;
EXCEPTION
WHEN OTHERS THEN
raise_application_error(-2000,sqlerrm);
END;
/



CREATE OR REPLACE FUNCTION DELETE_ALL_PRODUCTS_FROM_DB RETURN NUMBER AS
rcount NUMBER;
error_mess VARCHAR2(20);
BEGIN
DELETE FROM Product;
rcount := SQL%ROWCOUNT;
return rcount;
EXCEPTION
WHEN OTHERS THEN
error_mess := sqlerrm;
raise_application_error(-20000,error_mess);
END;
/
CREATE OR REPLACE PROCEDURE DELETE_ALL_PRODUCTS_VIASQLDEV AS
rcount NUMBER;
err_mess VARCHAR(20);
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Deleting all Product rows');
rcount := DELETE_ALL_PRODUCTS_FROM_DB;
dbms_output.put_line(rcount||' rows deleted');
EXCEPTION
WHEN OTHERS THEN
err_mess := sqlerrm;
dbms_output.put_line(err_mess);
END;
/
---TASK 1.4 -----------------------------------------------------------------

CREATE OR REPLACE FUNCTION GET_CUST_STRING_FROM_DB(pcustid NUMBER) RETURN VARCHAR2 IS
statement_line VARCHAR2(200);
errmess VARCHAR(50); 
BEGIN
SELECT 'CustId: '||customer.custid||' '||'Name: '||customer.custname||' '||'Status: '||customer.status||' '||'SalesYTD: '||customer.sales_ytd INTO statement_line
FROM CUSTOMER WHERE custid = pcustid;
RETURN statement_line;
EXCEPTION
WHEN NO_DATA_FOUND THEN
raise_application_error(-20062,'Customer ID not found');
WHEN OTHERS THEN
errmess := sqlerrm;
raise_application_error(-20000,errmess);
END;
/

CREATE OR REPLACE PROCEDURE GET_CUST_STRING_VIASQLDEV(pcustid NUMBER) AS
errmess VARCHAR2(100);
getcus VARCHAR2(100);
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Getting Details for CustId '||pcustid);
getcus := GET_CUST_STRING_FROM_DB(pcustid);
dbms_output.put_line(getcus);
EXCEPTION
WHEN OTHERS THEN
errmess := sqlerrm;
dbms_output.put_line(errmess);
END;
/


CREATE OR REPLACE PROCEDURE UPD_CUST_SALESYTD_IN_DB(pcustid NUMBER,pamt NUMBER) AS
rcount NUMBER;
errmess VARCHAR(20);
no_rows_updated EXCEPTION;
out_of_range EXCEPTION;
BEGIN
IF pamt BETWEEN -999.99 AND 999.99 THEN
UPDATE Customer SET SALES_YTD = pamt WHERE CUSTID = pcustid;
rcount := SQL%ROWCOUNT;
IF rcount = 0 THEN
RAISE no_rows_updated;
END IF;
ELSE
RAISE out_of_range;
END IF;
EXCEPTION
WHEN no_rows_updated THEN
raise_application_error(-20072,' Customer ID not found');
WHEN out_of_range THEN
raise_application_error(-20084,' Amount of of Range');
WHEN OTHERS THEN
errmess := sqlerrm;
raise_application_error(-20000,errmess);
END;
/
CREATE OR REPLACE PROCEDURE UPD_CUST_SALESYTD_VIASQLDEV(pcustid NUMBER,pamt NUMBER) AS
errmess VARCHAR(20);
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Updating SalesYTD. Customer Id: '||pcustid||' Amount: '||pamt);
UPD_CUST_SALESYTD_IN_DB(pcustid,pamt);
dbms_output.put_line('Update OK');
COMMIT;
EXCEPTION
WHEN OTHERS THEN
errmess := sqlerrm;
dbms_output.put_line(errmess);
END;
/


----- TASK 1.5 ----------------------------------------------

CREATE OR REPLACE FUNCTION GET_PROD_STRING_FROM_DB(pprodid NUMBER) RETURN VARCHAR2 IS
statement_one VARCHAR2(100);
errmess VARCHAR2(50);
BEGIN
SELECT 'Prodid:'||Product.PRODID||' '||'Name: '||Product.PRODNAME||' '||'Price: '||Product.SELLING_PRICE|| ' '||'SalesYTD: '||Product.SALES_YTD  
INTO statement_one FROM product WHERE prodid = pprodid;
RETURN statement_one;
EXCEPTION
WHEN NO_DATA_FOUND THEN
raise_application_error(-20092,'Product ID not found');
WHEN OTHERS THEN 
errmess := sqlerrm;
raise_application_error(-20000,errmess);
END;
/
CREATE OR REPLACE PROCEDURE GET_PROD_STRING_VIASQLDEV(pprodid NUMBER) AS 
stmnt VARCHAR2(100);
errmess Varchar2(20);
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Getting Details for Prod Id '||pprodid);
stmnt := GET_PROD_STRING_FROM_DB(pprodid);
dbms_output.put_line(stmnt);
EXCEPTION
WHEN OTHERS THEN
errmess := sqlerrm;
dbms_output.put_line(errmess);
END;
/

CREATE OR REPLACE PROCEDURE UPD_PROD_SALESYTD_IN_DB(pprodid NUMBER, pamt NUMBER) AS
rcount NUMBER(20);
no_rows_updated EXCEPTION;
out_of_range EXCEPTION;
errmess VARCHAR2(20);
BEGIN
IF pamt BETWEEN -999.99 AND 999.99 THEN
UPDATE PRODUCT SET Sales_YTD = pamt WHERE prodid = pprodid;
rcount := SQL%ROWCOUNT;
IF rcount < 1 THEN
RAISE no_rows_updated;
END IF;
ELSE
RAISE out_of_range;
END IF;
EXCEPTION
WHEN no_rows_updated THEN
raise_application_error(-20102,' Product ID not found');
WHEN out_of_range THEN
raise_application_error(-20114,' Amount of of Range');
WHEN OTHERS THEN
errmess := sqlerrm;
raise_application_error(-20000,errmess);
END;
/
CREATE OR REPLACE PROCEDURE UPD_PROD_SALESYTD_VIASQLDEV(pprodid NUMBER,pamt NUMBER) AS
rcount NUMBER;
errmess VARCHAR2(20);
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Updating SalesYTD Product Id: '||pprodid||' Amount: '||pamt);
UPD_PROD_SALESYTD_IN_DB(pprodid,pamt);
rcount := SQL%ROWCOUNT;
IF rcount > 0 THEN
dbms_output.put_line('Update OK');
END IF;
EXCEPTION
WHEN OTHERS THEN
errmess := sqlerrm;
dbms_output.put_line(errmess);
END;
/


---- TASK 1.6 --------------------------------------------------------------

CREATE OR REPLACE PROCEDURE UPD_CUST_STATUS_IN_DB(pcustid NUMBER,pstatus VARCHAR2) AS
rcount NUMBER;
invalid_status EXCEPTION;
no_rows_updated EXCEPTION;
errm VARCHAR2(20);
BEGIN
IF pstatus = 'OK' THEN
UPDATE Customer SET status = pstatus WHERE custid = pcustid;
ELSIF pstatus = 'SUSPEND' THEN
UPDATE Customer SET status = pstatus WHERE custid = pcustid;
rcount := SQL%ROWCOUNT;
IF rcount < 1 THEN
RAISE no_rows_updated;
END IF;
ELSE
RAISE invalid_status;
END IF;
EXCEPTION
WHEN no_rows_updated THEN
raise_application_error(-20122,' Customer ID not found');
WHEN invalid_status THEN
raise_application_error(-20134,'Invalid Status value');
WHEN OTHERS THEN
errm := sqlerrm;
Raise_application_error(-20000,errm);
END;
/
CREATE OR REPLACE PROCEDURE UPD_CUST_STATUS_VIASQLDEV(PCUSTID NUMBER,PSTATUS VARCHAR2) AS
rcount NUMBER;
errm varchar2(20);
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Updating Status. Id: '||pcustid||' New Status: '||pstatus);
UPD_CUST_STATUS_IN_DB(pcustid,pstatus);
rcount := SQL%ROWCOUNT;
IF rcount > 0 THEN
dbms_output.put_line('Update OK');
COMMIT;
END IF;
EXCEPTION
WHEN OTHERS THEN
errm := sqlerrm;
dbms_output.put_line(errm);
END;
/


--- TASK 1.7 ---------------------------------------------------------
CREATE OR REPLACE PROCEDURE ADD_SIMPLE_SALE_TO_DB(pcustid NUMBER,pprodid NUMBER,pqty NUMBER) AS
vcstatus VARCHAR2(10);
sellp NUMBER;
pamount NUMBER;
invalid_status EXCEPTION;
invalid_qty EXCEPTION;
errmess VARCHAR2(20);
BEGIN
SELECT customer.status INTO vcstatus FROM CUSTOMER WHERE custid = pcustid;
IF vcstatus != 'OK' THEN
RAISE invalid_status;
ELSIF pqty NOT BETWEEN 1 AND 999 THEN
RAISE invalid_qty;
ELSE
SELECT product.selling_price INTO sellp FROM product WHERE prodid = pprodid;
pamount := sellp*pqty;
UPD_CUST_SALESYTD_IN_DB(pcustid,pamount);
UPD_PROD_SALESYTD_IN_DB(pprodid,pamount);
END IF;
EXCEPTION
WHEN invalid_status THEN
raise_application_error(-20154,'. Customer Status is not OK');
WHEN invalid_qty THEN
raise_application_error(-20142,'. Sale Quantity outside valid range');
WHEN NO_DATA_FOUND THEN
raise_application_error(-20178,'. Product ID not found');
WHEN OTHERS THEN
errmess := sqlerrm;
raise_application_error(-20000,sqlerrm);
END;
/

CREATE OR REPLACE PROCEDURE ADD_SIMPLE_SALE_VIASQLDEV(pcustid NUMBER,pprodid NUMBER, pqty NUMBER) AS

BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Adding Simple Sale. Cust Id: '||pcustid||' Prod Id '||pprodid||'Qty: '||pqty);
ADD_SIMPLE_SALE_TO_DB(pcustid,pprodid,pqty);
dbms_output.put_line('Added Simple Sale OK');
END;
/

----TASK 1.8 ---------------------------------------------------------

CREATE OR REPLACE FUNCTION SUM_CUST_SALESYTD RETURN NUMBER IS 
finalval NUMBER;
BEGIN
SELECT SUM(Sales_YTD) INTO finalval FROM CUSTOMER;
return finalval;
END;
/
CREATE OR REPLACE PROCEDURE SUM_CUST_SALES_VIASQLDEV AS
pvalue NUMBER;
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Summing Customer SalesYTD');
pvalue := SUM_CUST_SALESYTD();
dbms_output.put_line('All Customer Total: '||pvalue);
EXCEPTION
WHEN NO_DATA_FOUND THEN
dbms_output.put_line('All Customer Total: 0');
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/
CREATE OR REPLACE FUNCTION SUM_PROD_SALESYTD_FROM_DB RETURN NUMBER IS 
finalval NUMBER;
BEGIN
SELECT SUM(Sales_YTD) INTO finalval FROM Product;
return finalval;
END;
/
CREATE OR REPLACE PROCEDURE SUM_PROD_SALES_VIASQLDEV AS
pvalue NUMBER;
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Summing Product SalesYTD');
pvalue := SUM_PROD_SALESYTD_FROM_DB();
dbms_output.put_line('All Product Total: '||pvalue);
EXCEPTION
WHEN NO_DATA_FOUND THEN
dbms_output.put_line('All Product Total: 0');
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/




-----TASK 2 ------------------------
CREATE OR REPLACE FUNCTION GET_ALLCUST RETURN SYS_REFCURSOR AS 
custcursor SYS_REFCURSOR;
BEGIN
OPEN custcursor FOR SELECT CUSTID ,
CUSTNAME ,
SALES_YTD ,
STATUS  FROM CUSTOMER;
RETURN custcursor;
EXCEPTION
WHEN OTHERS THEN
raise_application_error(-20000,sqlerrm);
END;
/

CREATE OR REPLACE PROCEDURE GET_ALLCUST_VIASQLDEV AS 
custcursor SYS_REFCURSOR;
custrec customer%ROWTYPE;
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Listing All Customer Details');
custcursor := GET_ALLCUST;
LOOP
    FETCH custcursor INTO custrec;
    EXIT WHEN custcursor%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Custid:'||custrec.custid||' Name: '||custrec.custname||' Status'||custrec.status||' SalesYTD: '||custrec.sales_ytd);
END LOOP;
END;
/

CREATE OR REPLACE FUNCTION GET_ALLPROD_FROM_DB RETURN SYS_REFCURSOR AS 
prodcursor SYS_REFCURSOR;
BEGIN
OPEN prodcursor FOR SELECT * FROM PRODUCT;
RETURN prodcursor;
EXCEPTION
WHEN OTHERS THEN
raise_application_error(-20000,sqlerrm);
END;
/

CREATE OR REPLACE PROCEDURE GET_ALLPROD_VIASQLDEV AS 
prodcursor SYS_REFCURSOR;
prodrec product%ROWTYPE;
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Listing All Product Details');
prodcursor := GET_ALLPROD_FROM_DB;
LOOP
    FETCH prodcursor INTO prodrec;
    EXIT WHEN prodcursor%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Prodid:'||prodrec.prodid||' Name: '||prodrec.prodname||' Price '||prodrec.selling_price||' SalesYTD: '||prodrec.sales_ytd);
END LOOP;
END;
/

-------- PART 3 -------------------

CREATE OR REPLACE PROCEDURE ADD_LOCATION_TO_DB(ploccode VARCHAR2,pminqty NUMBER,pmaxqty NUMBER) AS
dbms_constraint_name VARCHAR2(1000);
invalid_loc EXCEPTION;
len NUMBER;
BEGIN
len := length(ploccode);
IF len != 5 THEN
raise invalid_loc;
ELSE
INSERT INTO LOCATION VALUES(ploccode,pminqty,pmaxqty);
END IF;
EXCEPTION
WHEN invalid_loc THEN
raise_application_error(-20194,'Location Code Length Invalid');
WHEN DUP_VAL_ON_INDEX THEN
raise_application_error(-20182,'Duplicate Location ID');
WHEN OTHERS THEN
dbms_constraint_name :=strip_constraint(SQLERRM);
IF dbms_constraint_name = 'CHECK_MINQTY_RANGE' THEN
raise_application_error(-20206,'Minimum Qty out of range');
ELSIF dbms_constraint_name = 'CHECK_MAXQTY_RANGE' THEN
raise_application_error(-20206,'Maximum Qty out of range');
ELSIF dbms_constraint_name = 'CHECK_MAXQTY_GREATER_MIXQTY' THEN
raise_application_error(-20229,'Minimum Qty larger than Maximum Qty');
ELSE
raise_application_error(-20000,sqlerrm);
END IF;
END;
/

CREATE OR REPLACE PROCEDURE ADD_LOCATION_VIASQLDEV(ploccode VARCHAR2,pminqty NUMBER,pmaxqty NUMBER) AS
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Ading Location LocCode:'||ploccode||'MinQty: '||pminqty||' MaxQty: '||pmaxqty);
ADD_LOCATION_TO_DB(ploccode,pminqty,pmaxqty);
if sql%rowcount > 0 THEN
dbms_output.put_line('Location Addded OK');
COMMIT;
END IF;
EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/


----- TASK 4-------------

CREATE OR REPLACE PROCEDURE ADD_COMPLEX_SALE_TO_DB(pcustid NUMBER,pprodid NUMBER,pqty NUMBER,pdate VARCHAR2) AS
pstatus VARCHAR2(10);
invalid_status EXCEPTION;
vprice NUMBER;
error_date EXCEPTION;
invalid_range EXCEPTION;
vdates DATE;
vcount NUMBER;
error_cust EXCEPTION;
error_prod EXCEPTION;
BEGIN
SELECT selling_price into vprice FROM PRODUCT WHERE prodid = pprodid;
SELECT count(*) INTO vcount FROM  CUSTOMER WHERE custid = pCustid;
IF vcount = 0 THEN
raise error_cust;
ELSE
vcount := 0;
END IF;

SELECT count(*) INTO vcount FROM  Product WHERE prodid = pprodid;
IF vcount = 0 THEN
raise error_prod;
ELSE
vcount := 0;
END IF;
SELECT status INTO pstatus FROM CUSTOMER WHERE custid = pcustid;
IF pstatus != 'OK' THEN
    raise invalid_status;
ELSE 
    IF pqty NOT BETWEEN 1 AND 999 THEN
    raise invalid_range;
    END IF;
    IF regexp_like(pdate,'YYYYMMDD') = FALSE THEN
    raise error_date;
    END IF;
    vdates := to_date(pdate,'YYYY-MM-DD');
    UPD_CUST_SALESYTD_IN_DB(pcustid,pqty*vprice);
    UPD_PROD_SALESYTD_IN_DB(pprodid,pqty*vprice);
    INSERT INTO SALE VALUES(SALE_SEQ.nextval,pcustid,pprodid,pqty,vprice,vdates);
    
END IF;
EXCEPTION
WHEN invalid_range THEN
raise_Application_error(-20232,'Sale Quantity outside valid range');
WHEN invalid_status THEN
raise_Application_error(-20244,'Customer status is not OK');
WHEN error_date THEN
raise_application_error(-20256,'Date not valid');

END;
/

CREATE OR REPLACE PROCEDURE ADD_COMPLEX_SALES_VIASQLDEV(pcustid NUMBER,pprodid NUMBER,pqty NUMBER,pdate Varchar2) AS
pamt NUMBER;  ---- DEFINE
vprice NUMBER;
BEGIN
SELECT selling_price into vprice FROM PRODUCT WHERE prodid = pprodid;
pamt := pqty*vprice;
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Adding Complex Sale. Cust Id: '||pcustid||' Prod Id '||pprodid||' Date '||pdate||' Amt:'||pamt);
ADD_COMPLEX_SALE_TO_DB(pcustid,pprodid,pqty,pdate);
IF sql%rowcount > 0 THEN
dbms_output.put_line('Added Complex Sale OK');
END IF;
EXCEPTION
WHEN OTHERS THEN 
dbms_output.put_line(sqlerrm);
END;
/

CREATE OR REPLACE FUNCTION GET_ALLSALES_FROM_DB RETURN SYS_REFCURSOR AS 
salescur SYS_REFCURSOR;
BEGIN
OPEN salescur FOR SELECT * FROM SALE;
RETURN salescur;
EXCEPTION
WHEN OTHERS THEN
raise_application_error(-20000,sqlerrm);
END;
/

CREATE OR REPLACE PROCEDURE GET_ALLSALES_VIASQLDEV AS 
salescur SYS_REFCURSOR;
salesrec SALE%ROWTYPE;
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Listing All Complex Sales Details');
salescur := GET_ALLSALES_FROM_DB;
LOOP
    FETCH salescur INTO salesrec;
    IF salescur%rowcount = 0 THEN
    dbms_output.put_line('No rows found');
    END IF;
    EXIT WHEN salescur%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Saleid:'||salesrec.saleid|| 'Custid: '||salesrec.custid||' Prodid: '||salesrec.prodid ||' Date: '||salesrec.saledate||' Amount:'||salesrec.price);
END LOOP;

END;
/

CREATE OR REPLACE FUNCTION COUNT_PRODUCT_SALES_FROM_DB(PDAYS NUMBER) RETURN NUMBER AS
salesnumber NUMBER;
daterange date;
totalcount NUMBER;
BEGIN
daterange := sysdate - pdays;
SELECT COUNT(*) into totalcount FROM SALE WHERE SALEDATE BETWEEN daterange and sysdate;
return totalcount;
END;
/

CREATE OR REPLACE PROCEDURE COUNT_PRODUCT_SALES_VIASQLDEV(pdays NUMBER) AS
vanswer NUMBER;
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Counting sales within'||pdays||' days');
vanswer := COUNT_PRODUCT_SALES_FROM_DB(pdays);
dbms_output.put_line('Total number of sales: '||vanswer);
EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/

--- TASK 5 ------

CREATE OR REPLACE FUNCTION DELETE_SALE_FROM_DB RETURN NUMBER AS
smallestval NUMBER;
no_sale_found EXCEPTION;
vcustid  NUMBER;
vprodid  NUMBER;
vqty NUMBER;
vprice NUMBER;
CUST_SALES_YTD number;
PROD_SALES_YTD NUMBER;
PAMOUNT NUMBER;

BEGIN
SELECT MIN(saleid) INTO smallestval FROM SALE;
IF smallestval = NULL THEN
raise no_sale_found;
ELSE

SELECT custid,prodid,qty,price INTO vcustid,vprodid,vqty,vprice FROM SALE WHERE saleid = smallestval;

SELECT SALES_YTD INTO CUST_SALES_YTD FROM CUSTOMER WHERE CUSTID = VCUSTID;

SELECT SALES_YTD INTO PROD_SALES_YTD FROM PRODUCT WHERE PRODID = VPRODID;

PAMOUNT := VPRICE*VQTY;

DELETE FROM SALE WHERE SALEID = SMALLESTVAL;

UPD_CUST_SALESYTD_IN_DB(VCUSTID,CUST_SALES_YTD-PAMOUNT);
UPD_PROD_SALESYTD_IN_DB(VPRODID,PROD_SALES_YTD-PAMOUNT);

end if;

RETURN SMALLESTVAL;
exception
WHEN no_sale_found THEN
RAISE_APPLICATION_ERROR(-20282,'No Sale Rows Found');
WHEN OTHERS THEN
RAISE_aPPLICATION_ERROR(-20000,SQLERRM);
END;
/

CREATE OR REPLACE PROCEDURE DELETE_SALE_VIASQLDEV AS
ANSWER NUMBER;
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Deleting Sale with smallest SaleId value');
ANSWER:= DELETE_SALE_FROM_DB;
if SQL%ROWCOUNT > 0 THEN
dbms_output.put_line('Deleted Sale OK. SaleID:'||ANSWER);
COMMIT;
END IF;
EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/

CREATE OR REPLACE PROCEDURE DELETE_ALL_SALES_FROM_DB as
BEGIN
delete from SALE;
IF SQL%ROWCOUNT > 0 THEN
UPDATE CUSTOMER SET SALES_YTD = 0;
UPDATE PRODUCT SET SALES_YTD = 0;
END IF;
EXCEPTION
WHEN OTHERS THEN
RAISE_APPLICATION_ERROR(-2000,SQLERRM);
END;
/
CREATE OR REPLACE PROCEDURE DELETE_ALL_SALES_VIASQLDEV as
BEGIN
dbms_output.put_line('--------------------------------------------');
DBMS_OUTPUT.PUT_LINE('Deleting all sales data in Sale,customer, and product tables');
DELETE_ALL_SALES_FROM_DB;
IF SQL%ROWCOUNT > 0 THEN
dbms_output.put_line('Deletion OK');
COMMIT;
END IF;

EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/

--- PART 6 ----

CREATE OR REPLACE PROCEDURE DELETE_CUSTOMER(pCustid NUMBER) AS
CUSTCOUNT NUMBER;
CUST_NOT_FOUND EXCEPTION;
CHILD_REC exception;
PRAGMA EXCEPTION_INIT(CHILD_REC,-2292);
BEGIN
SELECT COUNT(*) INTO CUSTCOUNT FROM CUSTOMER WHERE CUSTID = PCUSTID;
IF CUSTCOUNT < 1 THEN
RAISE CUST_NOT_FOUND;
end if;
DELETE FROM CUSTOMER WHERE CUSTID = PCUSTID;

EXCEPTION
WHEN CUST_NOT_FOUND THEN
RAISE_APPLICATION_ERROR(-2292,'Customer ID not found');
WHEN CHILD_REC THEN
RAISE_APPLICATION_ERROR(-20304,'Customer cannot be deleted as sales');
WHEN OTHERS THEN
RAISE_APPLICATION_ERROR(-20000,SQLERRM);
END;
/

CREATE OR REPLACE PROCEDURE DELETE_CUSTOMER_VIASQLDEV(pCustid NUMBER) AS
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Deleting Customer. Cust Id: '||pCustid);
DELETE_CUSTOMER(pCustid);
IF SQL%ROWCOUNT > 0 THEN
dbms_output.put_line('Deleted Customer OK');
COMMIT;
end IF;
EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/


CREATE OR REPLACE PROCEDURE DELETE_PROD_FROM_DB (pProdid NUMBER) AS
PRODCOUNT NUMBER;
PROD_NOT_FOUND EXCEPTION;
CHILD_REC_ERR exception;
PRAGMA EXCEPTION_INIT(CHILD_REC_ERR,-2292);
BEGIN
SELECT COUNT(*) INTO PRODCOUNT FROM PRODUCT WHERE prodid = pprodid;
IF PRODCOUNT < 1 THEN
RAISE PROD_NOT_FOUND;
end if;
DELETE FROM PRODUCT WHERE prodid = PprodID;

EXCEPTION
WHEN PROD_NOT_FOUND THEN
RAISE_APPLICATION_ERROR(-20312,'Product ID not found');
WHEN CHILD_REC_err THEN
RAISE_APPLICATION_ERROR(-20304,'Product cannot be deleted as sales');
WHEN OTHERS THEN
RAISE_APPLICATION_ERROR(-20000,SQLERRM);
END;
/

CREATE OR REPLACE PROCEDURE DELETE_PROD_VIASQLDEV(pprodid NUMBER) AS
BEGIN
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Deleting Product. Product Id: '||pprodid);
DELETE_CUSTOMER(pprodid);
IF SQL%ROWCOUNT > 0 THEN
dbms_output.put_line('Deleted Product OK');
COMMIT;
end IF;
EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
END;
/
