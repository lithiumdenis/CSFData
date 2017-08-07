(defclass tickets-automate
  (is-a USER)
  (role concrete)
  (pattern-match reactive)
  (slot ticketsonconcert1 (type INTEGER) (create-accessor read-write))
  (slot ticketsonconcert2 (type INTEGER) (create-accessor read-write))
)

(deffacts world
(money 3)
)

(definstances automates
  (our-automate of tickets-automate
    (ticketsonconcert1 2)
	(ticketsonconcert2 4)
  )
)

(defmessage-handler tickets-automate getticket (?money)
  (if (= ?money 1) then
    (if (> (dynamic-get ticketsonconcert1) 0) then
      (dynamic-put ticketsonconcert1 (- (dynamic-get ticketsonconcert1) 1))
      (printout t "Your ticket on concert 1, please" crlf)
    else
      (printout t "Sorry, no more tickets on concert 1" crlf)
    )
	else
    (if (= ?money 3) then
      (if (> (dynamic-get ticketsonconcert2) 0) then
          (dynamic-put ticketsonconcert2 (- (dynamic-get ticketsonconcert2) 1))
          (printout t "Your ticket on concert 2, please" crlf)
	  else
          (printout t "Sorry, no more tickets on concert 2" crlf)
      )
    else
      (printout t "Wrong money" crlf)
    )
  )
)

(defrule catchticket
  (money ?money)
  =>
  (send [our-automate] getticket ?money)
)