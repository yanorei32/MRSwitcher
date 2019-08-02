CSC		= /cygdrive/c/windows/microsoft.net/framework/v4.0.30319/csc.exe
TARGET	= MR.exe
SRC		= \
	main.cs \

DEPS	= \
	manifest.xml \

CSC_FLAGS		= /nologo /win32manifest:manifest.xml /target:winexe
DEBUG_FLAGS		= 
RELEASE_FLAGS	= 

.PHONY: debug
debug: CSC_FLAGS+=$(DEBUG_FLAGS)
debug: all

.PHONY: release
release: CSC_FLAGS+=$(RELEASE_FLAGS)
release: all

all: $(TARGET)
$(TARGET): $(SRC) $(DEPS)
	$(CSC) $(CSC_FLAGS) /out:$(TARGET) $(SRC) | iconv -f cp932 -t utf-8

.PHONY: clean
clean:
	rm $(TARGET)


