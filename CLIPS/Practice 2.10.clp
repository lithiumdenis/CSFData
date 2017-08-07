(deffacts situation
	  (Max is four)
)

(defrule Idea1
(Max is first)
=>
(assert (Bill isn't second))
)

(defrule Idea2
(Max isn't first)
=>
(assert (Bill is second))
)

(defrule Idea3
(Bill is third)
=>
(assert (Nik isn't first))
)

(defrule Idea4
(Bill isn't third)
=>
(assert (Nik is first))
)

(defrule Idea5
(Max is four)
=>
(assert (John isn't first))
)

(defrule Idea6
(Max isn't four)
=>
(assert (John is first))
)

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
(defrule Idea7
(Max is first)
=>
(assert (Bill isn't first))
(assert (Nik isn't first))
(assert (John isn't first))
)

(defrule Idea8
(Max is second)
=>
(assert (Bill isn't second))
(assert (Nik isn't second))
(assert (John isn't second))
)

(defrule Idea9
(Max is third)
=>
(assert (Bill isn't third))
(assert (Nik isn't third))
(assert (John isn't third))
)

(defrule Idea10
(Max is four)
=>
(assert (Bill isn't four))
(assert (Nik isn't four))
(assert (John isn't four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
(defrule Idea11
(Bill is first)
=>
(assert (Max isn't first))
(assert (Nik isn't first))
(assert (John isn't first))
)

(defrule Idea12
(Bill is second)
=>
(assert (Max isn't second))
(assert (Nik isn't second))
(assert (John isn't second))
)

(defrule Idea13
(Bill is third)
=>
(assert (Max isn't third))
(assert (Nik isn't third))
(assert (John isn't third))
)

(defrule Idea14
(Bill is four)
=>
(assert (Max isn't four))
(assert (Nik isn't four))
(assert (John isn't four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

(defrule Idea15
(Nik is first)
=>
(assert (Max isn't first))
(assert (Bill isn't first))
(assert (John isn't first))
)

(defrule Idea16
(Nik is second)
=>
(assert (Max isn't second))
(assert (Bill isn't second))
(assert (John isn't second))
)

(defrule Idea17
(Nik is third)
=>
(assert (Max isn't third))
(assert (Bill isn't third))
(assert (John isn't third))
)

(defrule Idea18
(Nik is four)
=>
(assert (Max isn't four))
(assert (Bill isn't four))
(assert (John isn't four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

(defrule Idea19
(John is first)
=>
(assert (Max isn't first))
(assert (Bill isn't first))
(assert (Nik isn't first))
)

(defrule Idea20
(John is second)
=>
(assert (Max isn't second))
(assert (Bill isn't second))
(assert (Nik isn't second))
)

(defrule Idea21
(John is third)
=>
(assert (Max isn't third))
(assert (Bill isn't third))
(assert (Nik isn't third))
)

(defrule Idea22
(John is four)
=>
(assert (Max isn't four))
(assert (Bill isn't four))
(assert (Nik isn't four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
(defrule Idea23
(Max is first)
=>
(assert (Max isn't second))
(assert (Max isn't third))
(assert (Max isn't four))
)

(defrule Idea24
(Max is second)
=>
(assert (Max isn't first))
(assert (Max isn't third))
(assert (Max isn't four))
)

(defrule Idea25
(Max is third)
=>
(assert (Max isn't first))
(assert (Max isn't second))
(assert (Max isn't four))
)

(defrule Idea26
(Max is four)
=>
(assert (Max isn't first))
(assert (Max isn't second))
(assert (Max isn't third))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

(defrule Idea27
(Bill is first)
=>
(assert (Bill isn't second))
(assert (Bill isn't third))
(assert (Bill isn't four))
)

(defrule Idea28
(Bill is second)
=>
(assert (Bill isn't first))
(assert (Bill isn't third))
(assert (Bill isn't four))
)

(defrule Idea29
(Bill is third)
=>
(assert (Bill isn't first))
(assert (Bill isn't second))
(assert (Bill isn't four))
)

(defrule Idea30
(Bill is four)
=>
(assert (Bill isn't first))
(assert (Bill isn't second))
(assert (Bill isn't third))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

(defrule Idea31
(Nik is first)
=>
(assert (Nik isn't second))
(assert (Nik isn't third))
(assert (Nik isn't four))
)

(defrule Idea32
(Nik is second)
=>
(assert (Nik isn't first))
(assert (Nik isn't third))
(assert (Nik isn't four))
)

(defrule Idea33
(Nik is third)
=>
(assert (Nik isn't first))
(assert (Nik isn't second))
(assert (Nik isn't four))
)

(defrule Idea34
(Nik is four)
=>
(assert (Nik isn't first))
(assert (Nik isn't second))
(assert (Nik isn't third))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

(defrule Idea35
(John is first)
=>
(assert (John isn't second))
(assert (John isn't third))
(assert (John isn't four))
)

(defrule Idea36
(John is second)
=>
(assert (John isn't first))
(assert (John isn't third))
(assert (John isn't four))
)

(defrule Idea37
(John is third)
=>
(assert (John isn't first))
(assert (John isn't second))
(assert (John isn't four))
)

(defrule Idea38
(John is four)
=>
(assert (John isn't first))
(assert (John isn't second))
(assert (John isn't third))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;для определения последнего
(defrule Idea39
(and (John isn't first) 
     (John isn't second) 
	 (John isn't four))
=>
(assert (John is third))
)

(defrule Idea40
(and (John isn't third) 
     (John isn't second) 
	 (John isn't four))
=>
(assert (John is first))
)

(defrule Idea41
(and (John isn't first) 
     (John isn't third) 
	 (John isn't four))
=>
(assert (John is second))
)

(defrule Idea42
(and (John isn't first) 
     (John isn't second) 
	 (John isn't third))
=>
(assert (John is four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
(defrule Idea43
(and (Nik isn't first) 
     (Nik isn't second) 
	 (Nik isn't four))
=>
(assert (Nik is third))
)

(defrule Idea44
(and (Nik isn't third) 
     (Nik isn't second) 
	 (Nik isn't four))
=>
(assert (Nik is first))
)

(defrule Idea45
(and (Nik isn't first) 
     (Nik isn't third) 
	 (Nik isn't four))
=>
(assert (Nik is second))
)

(defrule Idea46
(and (Nik isn't first) 
     (Nik isn't second) 
	 (Nik isn't third))
=>
(assert (Nik is four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
(defrule Idea47
(and (Bill isn't first) 
     (Bill isn't second) 
	 (Bill isn't four))
=>
(assert (Bill is third))
)

(defrule Idea48
(and (Bill isn't third) 
     (Bill isn't second) 
	 (Bill isn't four))
=>
(assert (Bill is first))
)

(defrule Idea49
(and (Bill isn't first) 
     (Bill isn't third) 
	 (Bill isn't four))
=>
(assert (Bill is second))
)

(defrule Idea50
(and (Bill isn't first) 
     (Bill isn't second) 
	 (Bill isn't third))
=>
(assert (Bill is four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
(defrule Idea51
(and (Max isn't first) 
     (Max isn't second) 
	 (Max isn't four))
=>
(assert (Max is third))
)

(defrule Idea52
(and (Max isn't third) 
     (Max isn't second) 
	 (Max isn't four))
=>
(assert (Max is first))
)

(defrule Idea53
(and (Max isn't first) 
     (Max isn't third) 
	 (Max isn't four))
=>
(assert (Max is second))
)

(defrule Idea54
(and (Max isn't first) 
     (Max isn't second) 
	 (Max isn't third))
=>
(assert (Max is four))
)
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

(defrule stop
(Max ? first)
(Max ? second)
(Max ? third)
(Max ? four)

(Bill ? first)
(Bill ? second)
(Bill ? third)
(Bill ? four)

(Nik ? first)
(Nik ? second)
(Nik ? third)
(Nik ? four)

(John ? first)
(John ? second)
(John ? third)
(John ? four)
=>
(halt)
)