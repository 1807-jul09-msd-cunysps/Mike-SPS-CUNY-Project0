SELECT CONCAT(p.firstname, ' ', p.lastname) AS Name,
	   CONCAT(addr.housenum, ' ', addr.street, ' ', addr.zipcode) AS Address,
	   CONCAT(ph.number, ' ', ph.ext) AS Phone
FROM person AS p
INNER JOIN phone AS ph ON (p.Id = ph.personID)
INNER JOIN address AS addr ON (p.Id = addr.personID)