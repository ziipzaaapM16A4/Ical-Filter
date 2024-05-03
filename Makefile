CSCOMPILER = dotnet
SOLUTION = Ical-Filter.sln
CONFIGURATION = Release

all: compile run 
 
compile: 
	$(CSCOMPILER) build -p:WarningLevel=0 -c $(CONFIGURATION) $(SOLUTION)
 
run: 
	$(CSCOMPILER) run --project /icalfilter/src -p:WarningLevel=0 -c $(CONFIGURATION)

test: 
	$(CSCOMPILER) run --project /icalfilter/src -p:WarningLevel=0 -c $(CONFIGURATION) test
 
clean: 
	rm -f *.exe 